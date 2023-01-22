using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(OTM.Startup))]

namespace OTM
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR();
        }
    }
}
