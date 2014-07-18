using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using QuizWebApp.Models;

namespace QuizWebApp.Hubs
{
    [HubName("Context")]
    public class ContextHub : Hub
    {
        public void UpdateCurrentState(ContextStateType state)
        {
            using (var db = new QuizWebAppDb())
            {
                var context = db.Contexts.First();
                context.CurrentState = state;

                // if change state to "3:show answer", judge to all players.
                if (state == ContextStateType.ShowCorrectAnswer)
                {
                    var answers = db
                        .Answers
                        .Where(a => a.QuestionID == context.CurrentQuestionID)
                        .ToList();
                    var currentQuestion = db.Questions.Find(context.CurrentQuestionID);
                    //★
                    var users = db.Users.ToList();

                    // ★正解は最初から決まってない ここで現在の質問の正解判定してDB更新　現在の質問の選択肢ごとの人数を全部集計する->正解選択肢きめる->負けプレイヤーのスコア合計->勝ちプレイヤーに配分
                    // Question更新，Users更新,AnswersのStatusをそれに応じて更新

                    //人数決定
                    Dictionary<int, int> playerCnt = new Dictionary<int, int>();
                    for(int i=1; i<=2; i++)
                        playerCnt.Add(i, answers.Count(a => a.QuestionID == context.CurrentQuestionID && a.ChoosedOptionIndex == i));

                    currentQuestion.Option1Count = playerCnt[1];
                    currentQuestion.Option2Count = playerCnt[2];
 
                    //勝ち決定　同数だった場合は1が正解
                    int minIndex=0,minCnt=100000;
                    bool draw = false;
                     foreach (KeyValuePair<int, int> kvp in playerCnt) {
                         if (kvp.Value == 0 || minCnt == kvp.Value)
                         {
                             draw = true;
                         }
                         else if(minCnt>kvp.Value){
                             minCnt = kvp.Value;
                             minIndex = kvp.Key;
                         }
                     }
                     if (draw == true) {
                         minIndex = 0;  minCnt = 100000;
                     }
                    currentQuestion.IndexOfCorrectOption = minIndex;

                    if (draw == false) { 
                    answers
                       .ForEach(a => a.Status =
                          a.ChoosedOptionIndex == currentQuestion.IndexOfCorrectOption
                         ? AnswerStateType.Correct : AnswerStateType.Incorrect);

                    //配布ポイント決定 answerで正解、不正解のユーザID取得->不正解ユーザのポイントを半分にする＋半分ずつを合計→正解ユーザで山分け　
                    var correctAnswers = answers.Where(a => a.QuestionID == context.CurrentQuestionID && a.Status == AnswerStateType.Correct).ToList();
                    var wrongAnswers = answers.Where(a => a.QuestionID == context.CurrentQuestionID && a.Status == AnswerStateType.Incorrect).ToList();
                 //   var noAnswers = answers.Where(a => a.QuestionID == context.CurrentQuestionID && a.ChoosedOptionIndex == -1).ToList();
                    List<string> correctPlayers = new List<string>();

                   int allDistributePoint = 0;;

                    //不正解ユーザの半分スコア合計
                    foreach(var a in wrongAnswers){
                        var playerID = a.PlayerID;
                        var user = users.First(u => u.UserId == playerID);
                        int score = user.Score;
                        allDistributePoint += score / 2;
          //              user.Score = score / 2;
                    }
                    
                    //配布ポイント計算
                     if (minCnt != 0)
                        currentQuestion.DistributePoint = allDistributePoint / minCnt;
                    else
                        currentQuestion.DistributePoint = 0;

                    //正解ユーザに配布
                     foreach (var a in correctAnswers)
                     {
                         var playerID = a.PlayerID;
                         var user = users.First(u => u.UserId == playerID);
                         user.Score += currentQuestion.DistributePoint;

                         correctPlayers.Add(playerID);
                     }

                    //正解に入ってないユーザの点数は半分に
                    foreach (var u in users)
                    {
                        if (correctPlayers.Contains(u.Score.ToString()) == false) {
                            int score = u.Score;
                            u.Score = score / 2;
                        }
                    }
                    }

  
                    //Answerのポイント更新 
                    answers
                       .ForEach(a => a.Point =  
                           a.ChoosedOptionIndex == currentQuestion.IndexOfCorrectOption
                       ? currentQuestion.DistributePoint : 0);                     
                }

                db.SaveChanges();
            }

            Clients.All.CurrentStateChanged(state.ToString());
        }

        public void PlayerSelectedOptionIndex(int answerIndex)
        {
            using (var db = new QuizWebAppDb())
            {
                var playerId = Context.User.Identity.UserId();
                var questionId = db.Contexts.First().CurrentQuestionID;
                var ansewer = db.Answers.First(a => a.PlayerID == playerId && a.QuestionID == questionId);
                ansewer.ChoosedOptionIndex = answerIndex;
                ansewer.Status = AnswerStateType.Pending;/*entried*/

                db.SaveChanges();
            }

            Clients.Others.PlayerSelectedOptionIndex();
        }
    }
}