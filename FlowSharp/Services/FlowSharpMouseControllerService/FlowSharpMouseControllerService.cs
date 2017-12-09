﻿/* 
* Copyright (c) Marc Clifton
* The Code Project Open License (CPOL) 1.02
* http://www.codeproject.com/info/cpol10.aspx
*/

using Clifton.Core.ModuleManagement;
using Clifton.Core.Semantics;
using Clifton.Core.ServiceManagement;

using FlowSharpLib;
using FlowSharpServiceInterfaces;

namespace FlowSharpMouseControllerService
{
    public class FlowSharpMouseControllerModule : IModule
    {
        public void InitializeServices(IServiceManager serviceManager)
        {
            serviceManager.RegisterSingleton<IFlowSharpMouseControllerService, FlowSharpMouseControllerService>();
        }
    }

    public class FlowSharpMouseControllerService : ServiceBase, IFlowSharpMouseControllerService
    {
        protected MouseController mouseController;

        public override void FinishedInitialization()
        {
            base.FinishedInitialization();
            ServiceManager.Get<ISemanticProcessor>().Register<FlowSharpMembrane, FlowSharpMouseControllerReceptor>();
            mouseController = new MouseController(ServiceManager);
            mouseController.InitializeBehavior();
        }

        public void Initialize(BaseController controller)
        {
            mouseController.HookMouseEvents(controller);
        }

        public void ClearState()
        {
            mouseController.ClearState();
        }

        public void ShapeDeleted(GraphicElement el)
        {
            mouseController.ShapeDeleted(el);
        }
    }

    public class FlowSharpMouseControllerReceptor : IReceptor
    {

    }

}
