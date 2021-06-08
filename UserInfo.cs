using System;

public class UserInfo
{
    public string firstname;
    public string lastname;
    public string email;
    public string phone;

    public UserInfo(string name, string surname, string mail, string number)
    {
        this.firstname = name;
        this.lastname = surname;
        this.email = mail;
        this.phone = number;
    }
}
