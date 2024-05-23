﻿using QuestionBank.Common.Enumeration;

namespace QuestionBank.Persistence.Entity
{
    public class QuestionFeedBack
    {
        public long Id { get; set; }

        public long QuestionId { get; set; }
        public Question Question { get; set; }
        public long  UserId{ get; set; }

        public UserAccount User { get; set; }

        public DateTime LastFeedBackSubmittedOn { get; set; }

        public QuestionFeedBackType FeedBackType { get; set; }

        public bool IsFeedBackCycleCompleted { get; set; } = false;


    }
}
