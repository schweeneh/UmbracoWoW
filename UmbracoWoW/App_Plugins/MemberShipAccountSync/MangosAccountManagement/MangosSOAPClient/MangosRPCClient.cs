using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ServiceModel;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement.MangosSOAPClient
{
    [ServiceContract()]
    public interface IMangosCommand
    {
        [OperationContract(Action = "execute_command")]
        string ExecuteCommand(string command);
    }

    public class MangosRPCClient
    {
        public string RunCommand(string command)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Namespace = "urn:MaNGOS";
            EndpointAddress endpoint = new EndpointAddress("http://localhost:7878");

            ChannelFactory<IMangosCommand> channelFactory = new ChannelFactory<IMangosCommand>(binding, endpoint);
            
            IMangosCommand client = channelFactory.CreateChannel();
            var result = client.ExecuteCommand(command);
            ((IClientChannel)client).Close();

            return result;
        }
       
    }
}