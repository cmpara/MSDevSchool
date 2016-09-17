using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace EmoticonBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                var rep =  Reply(activity.Text);
                Activity reply = activity.CreateReply(rep);
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        
        
       public string Reply(string msg)
        {
            string message = Regex.Replace(msg.ToLower().Trim(), "[^a-z ]","");
            
            if (message.IsHello())
                return Utils.SayHello()+"\n\r(hi)";
            if (message.IsGoodBye())
                return Utils.SayBye()+"\n\r(bye)";
            if (message.IsThanks())
                return Utils.SayWelcome() +"\n\r(thanks)";
            

            if (message.IsSwearWord())
                return "You said (wtf)\n\r"+Utils.SayNoSwear();
            
            if (message.IsHelp())
            {
                return "I am a simple emoticon suggesting bot :).\n\rYou can: \n\rgreet me (hi) \n\rsay good bye to me (bye) \n\rthank me (thanks) \n\r \n\rAnd don't be dirty (wtf)(talktothehand)";
            }


           
                return  string.Format("Here is my emoticon parapharasing: \n\r{0}", Utils.Rephrase(message));
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}