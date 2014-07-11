using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApp.Models
{
    //　★
    public class Question
    {
        public int QuestionId { get; set; }

        public string OwnerUserId { get; set; }

        [Display(Name = "問題本文"), Required, AllowHtml]
        public string Body { get; set; }
        [Display(Name = "問題画像")]
        public string BodyImage { get; set; }

        [Display(Name = "問題本文の書式"), Required]
        public TextFormatType BodyFormat { get; set; }

        [Display(Name = "回答選択肢1"), Required, AllowHtml]
        public string Option1 { get; set; }
        [Display(Name = "回答選択肢1人数")]
        public int Option1Count { get; set; }

        [Display(Name = "回答選択肢2"), Required, AllowHtml]
        public string Option2 { get; set; }
        [Display(Name = "回答選択肢2人数")]
        public int Option2Count { get; set; }


        public OptionViewModel[] GetOptions(bool trim = true)
        {
            Func<OptionViewModel, bool> filter = trim ?
                (Func<OptionViewModel, bool>)(opt => string.IsNullOrEmpty(opt.Option) == false) :
                (Func<OptionViewModel, bool>)(_ => true);

            return new[] { 
                new OptionViewModel(1, Option1),
                new OptionViewModel(2, Option2),
            }.Where(filter).ToArray();
        }

        [Display(Name = "正解の選択肢の番号")]
        public int IndexOfCorrectOption { get; set; }

        //★割り振られるべきポイント
        [Display(Name = "正解者への割り振りポイント")]
        public int DistributePoint { get; set; }

        [Display(Name = "解説"), AllowHtml]
        public string Comment { get; set; }

        [Display(Name = "問題本文の書式"), Required]
        public TextFormatType CommentFormat { get; set; }

        //public string Category { get; set; }

        [Display(Name = "投稿日時")]
        public DateTime CreateAt { get; set; }

        public Question()
        {
            this.CreateAt = DateTime.UtcNow;
        }
    }
}