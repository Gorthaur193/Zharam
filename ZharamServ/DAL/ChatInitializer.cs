﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ZharamServ.DAL
{
    public class ChatInitializer : DropCreateDatabaseIfModelChanges<ChatContext>
    {
        protected override void Seed(ChatContext context)
        {
             
        }
    }
}