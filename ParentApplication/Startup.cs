﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ParentApplication.Startup))]
namespace ParentApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
