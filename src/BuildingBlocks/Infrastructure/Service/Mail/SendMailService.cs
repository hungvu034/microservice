using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Services;
using Infrastructure.Configurations;
using Shared.Service.Mail;
using MailKit.Net.Smtp ;
using MimeKit;
using Serilog ; 
namespace Infrastructure.Service.Mail
{
    public class SendMailService : ISendMailService
    {
        private readonly EmailSMTPSettings _settings ;
        private readonly ILogger _logger ;  
        public SendMailService(EmailSMTPSettings settings , ILogger logger)
        {
            _settings = settings;
            _logger = logger ;
            _client = new SmtpClient();
        }

        private readonly SmtpClient _client ; 
        public async Task SendMailAsync(MailRequest request)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.DisplayName , _settings.From));
            message.Body = new BodyBuilder(){
                TextBody = request.Body
            }.ToMessageBody();
            if(!string.IsNullOrEmpty(request.Cc))
                message.Cc.Add(MailboxAddress.Parse(request.Cc));
            List<MailboxAddress> mailboxes = new List<MailboxAddress>();
            request.To.Aggregate(mailboxes , (mailboxes , item) => mailboxes.Append(MailboxAddress.Parse(item)).ToList());
            message.To.AddRange(mailboxes);
            message.Date = DateTimeOffset.Now ;
            message.Subject = request.Subject ; 
            try{
                await _client.ConnectAsync(_settings.SMTPServer , _settings.Port , _settings.UseSsl);
                Console.WriteLine("Xong connect");
                _logger.Information(_settings.UserName + _settings.Password);
                await _client.AuthenticateAsync(_settings.UserName , _settings.Password);
                await _client.SendAsync(message);
            }
            catch(Exception ex){
                _logger.Error(ex.Message);
                throw ; 
            }
            finally{
                _client.Disconnect(true);
                _client.Dispose();
            }
        }
    }
}