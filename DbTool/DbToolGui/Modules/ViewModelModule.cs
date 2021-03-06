﻿using DbTool.Lib;
using DbTool.Lib.Syntax;
using Ninject.Modules;

namespace DbToolGui.Modules
{
    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISyntaxProvider>().To<DbToolSyntaxProvider>().InSingletonScope();
            Bind<ISyntaxParser>().To<DbToolSyntaxParser>().InSingletonScope();
            Bind<IMetaInfoProvider>().To<MetaInfoProvider>().InSingletonScope();
        }
    }
}