using required.Modals;
using Microsoft.Extensions.Options;

namespace required.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> newBookAlertConfiguration;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfiguration)
        {
            this.newBookAlertConfiguration = newBookAlertConfiguration;

            //newBookAlertConfiguration.OnChange(config => {
            //    this.newBookAlertConfiguration = config;
            //});
        }
        public string GetName()
        {
            return newBookAlertConfiguration.CurrentValue.BookName;
        }
    }
}
