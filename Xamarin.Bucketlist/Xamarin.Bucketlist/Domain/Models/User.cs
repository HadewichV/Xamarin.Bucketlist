using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Bucketlist.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

    }
}
