using System;

// This file contains ship modules exeptions, so devs can know if something goes wrong.

namespace InFlood.Entities.Modules
{
    public class InitializeModuleException : Exception
    {
        public Module Module { get; }

        public InitializeModuleException(string message, Module module) : base(message)
        {
            Module = module;
        }
    }


    public class NoSlotListForModuleTypeExeption : Exception
    {
        public ModulesManager ModulesManager { get; }
        public Module Module { get; }

        public NoSlotListForModuleTypeExeption(string message, ModulesManager modulesManager, Module module) : base(message) 
        {
            ModulesManager = modulesManager;
            Module = module;
        }
    }


    public class ModuleAddRemoveExeption : Exception
    {
        public ModulesManager ModulesManager { get; }
        public Module Module { get; }


        public ModuleAddRemoveExeption(string message) : base(message) {}


        public ModuleAddRemoveExeption(string message, Module module) : this(message) 
        {
            Module = module;
        }


        public ModuleAddRemoveExeption(string message, ModulesManager modulesManager, Module module) : this(message, module) 
        {
            ModulesManager = modulesManager;
        }
    }

}