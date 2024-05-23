﻿namespace QuestionBank.Persistence.Entity;

public class Role
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }


    public ICollection<UserAccount>  UserAccounts { set; get; }
}