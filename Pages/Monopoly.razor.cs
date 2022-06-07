namespace MonopolyMoneyManager.Pages
{
using System.Collections.Generic;
using System.Threading.Tasks;
//using System.Text;
using System;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using TestBlazorWebAss.BusinessController;
using MonopolyMoneyManager.BusinessController;
using MonopolyMoneyManager.BusinessObjects;
using Microsoft.JSInterop;

    public partial class Monopoly
    {
        protected override async Task OnInitializedAsync()
        {
            init();

        }
        TransferController transferController = new TransferController();
        IList<GamerController> GamerControllers = new List<GamerController>();
        
        public void init(){
            foreach(var gamer in currentGamers.GamerList){
                GamerControllers.Add(new GamerController(gamer));
            }
            //GamerControllers.Add(new GamerController());
            //GamerControllers.Add(new GamerController());
            //GamerControllers.Add(new GamerController();
            //GamerControllers.Add(new GamerController(new Gamer(){Name = "T", Balance=100}));
            //GamerControllers.Add(new GamerController(new Gamer(){Name = "K", Balance=100}));
            //StateController<IList<GamerController>> stateController = new StateController<IList<GamerController>>(GamerControllers);
            foreach (var g in GamerControllers)
            {
                g.OnSenderChanged += () => transferController.ChangeSender(g);
                g.OnReceiverChanged += ()=> transferController.ChangeReceiver(g);
                transferController.OnResetSender += (gc) => g.ResetSender(gc);
                //transferController.OnResetAll += () => stateController.SaveStateLocal();
                transferController.OnResetAll += () => g.Reset();
            }
            transferController.UpdateGui();
        }

    }

}