//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SongHaven
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Messages = new HashSet<Message>();
        }
    
        public System.Guid guid_id { get; set; }
        public string nvc_username { get; set; }
        public string nvc_password { get; set; }
        public string nvc_first_name { get; set; }
        public string nvc_last_name { get; set; }
        public string nvc_email { get; set; }
        public System.DateTime dt_created_date { get; set; }
        public int int_account_strikes { get; set; }
        public Nullable<System.DateTime> dt_date_banned { get; set; }
    
        public virtual ICollection<Message> Messages { get; set; }
    }
}
