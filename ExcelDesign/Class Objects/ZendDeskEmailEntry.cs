using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// V10 - PAB 19 March 2019
/// Object for holding the email address returned by ZendDesk.
/// </summary>
public class ZendDeskEmailEntry
{
    /*V10 - PAB 19 March 2019
      Object for holding the email address returned by ZendDesk.
    */

    public string Name { get; set; }
    public string Address { get; set; }

    public ZendDeskEmailEntry(string NewName, string NewEmail)
    {
        this.Name = NewName;
        this.Address = NewEmail;
    }

    public ZendDeskEmailEntry()
    {

    }
}