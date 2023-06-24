using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.EmailSender
{
    public class EmailTemplate
    {
        public static string GetHtmlTemplate(string clientName, string orderNumber)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset=""UTF-8"">
                <title>Template de E-mail</title>
                <style>
                    body {{
                        margin: 0;
                        padding: 0;
                    }}

                    .header {{
                        background-color: yellow;
                        padding: 20px;
                        text-align: center;
                    }}

                    .header h1 {{
                        color: black;
                    }}

                    .header h2 {{
                        color: black;
                    }}

                    .footer {{
                        background-color: yellow;
                        padding: 20px;
                        text-align: center;
                    }}

                    .content {{
                        text-align: center;
                     }}

                    .footer p {{
                        color: black;
                    }}
                </style>
            </head>
            <body>
                <div class=""header"">
                    <h1>LesBooks</h1>
                    <h2>Avisos</h2>
                </div>

                <div class=""content"">
                    <p>Olá {clientName},</p>
                    <p>Seu pedido de troca {orderNumber} foi aprovado.</p>
                </div>

                <div class=""footer"">
                    <p>LesBooks - Todos os direitos reservados.</p>
                </div>
            </body>
            </html>
        ";
        }
    }

}
