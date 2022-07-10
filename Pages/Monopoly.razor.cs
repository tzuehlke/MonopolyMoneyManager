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
            Console.WriteLine("config is: "+config??"no config");
            if(!String.IsNullOrEmpty(config)){
                currentGamers.SetStateFromJSON(config);
            }
            init();

        }
        TransferController transferController = new TransferController();
        IList<GamerController> GamerControllers = new List<GamerController>();
        
        public void init(){
            foreach(var gamer in currentGamers.GamerList){
                GamerControllers.Add(new GamerController(gamer));
            }

            foreach (var g in GamerControllers)
            {
                g.OnSenderChanged += () => transferController.ChangeSender(g);
                g.OnReceiverChanged += ()=> transferController.ChangeReceiver(g);
                transferController.OnResetSender += (gc) => g.ResetSender(gc);
                //transferController.OnResetAll += () => stateController.SaveStateLocal();
                transferController.OnResetAll += () => g.Reset();
            }
            transferController.OnResetAll += () => directLink.GenerateLink();
            transferController.UpdateGui();
        }

    }

}