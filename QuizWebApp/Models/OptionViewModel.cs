using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebApp.Models
{
    public class OptionViewModel
    {
        public int OptionNumber { get; set; }

        public string Option { get; set; }

        public OptionViewModel(int optionNumber, string option)
        {
            this.OptionNumber = optionNumber;
            this.Option = option;
        }
    }
}