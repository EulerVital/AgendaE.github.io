using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AgendaE.Core.Helper
{
    public class FirebaseConfigucao
    {
        public IFirebaseConfig config { get; set; }

        public FirebaseConfigucao(IConfiguration configuration)
        {
            if (configuration == null)
            {
                config = new FirebaseConfig();
                return;
            }

            config = new FirebaseConfig
            {
                AuthSecret = configuration["Firebase:Secrets"], // Substitua pelo seu secret key
                BasePath = configuration["Firebase:DatabaseUrl"]
            };
        }
    }
}
