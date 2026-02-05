package com.example.proyecto.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Service;
import jakarta.mail.MessagingException;
import jakarta.mail.internet.MimeMessage;

@Service
public class EmailService {

    @Autowired
    private JavaMailSender mailSender;

    @Value("${spring.mail.username}")
    private String fromEmail;

    @Value("${app.name}")
    private String appName;

    /**
     * Envía un correo simple de texto plano
     */
    public void sendSimpleEmail(String to, String subject, String text) {
        SimpleMailMessage message = new SimpleMailMessage();
        message.setFrom(fromEmail);
        message.setTo(to);
        message.setSubject(subject);
        message.setText(text);

        mailSender.send(message);
    }

    /**
     * Envía un correo HTML con el PIN de recuperación
     */
    public void sendPasswordResetEmail(String to, String pin, String userName) throws MessagingException {
        MimeMessage message = mailSender.createMimeMessage();
        MimeMessageHelper helper = new MimeMessageHelper(message, true, "UTF-8");

        helper.setFrom(fromEmail);
        helper.setTo(to);
        helper.setSubject("Recuperación de Contraseña - " + appName);

        String htmlContent = buildPasswordResetEmailHtml(pin, userName);
        helper.setText(htmlContent, true);

        mailSender.send(message);
    }

    /**
     * Construye el HTML del correo de recuperación de contraseña
     */
    private String buildPasswordResetEmailHtml(String pin, String userName) {
        return "<!DOCTYPE html>" +
                "<html>" +
                "<head>" +
                "    <meta charset='UTF-8'>" +
                "    <meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                "</head>" +
                "<body style='margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f4f4f4;'>" +
                "    <table width='100%' cellpadding='0' cellspacing='0' style='background-color: #f4f4f4; padding: 20px;'>" +
                "        <tr>" +
                "            <td align='center'>" +
                "                <table width='600' cellpadding='0' cellspacing='0' style='background-color: #ffffff; border-radius: 10px; overflow: hidden; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>" +
                "                    <!-- Header -->" +
                "                    <tr>" +
                "                        <td style='background: linear-gradient(135deg, #6750A4 0%, #8B7DC8 100%); padding: 40px 30px; text-align: center;'>" +
                "                            <h1 style='color: #ffffff; margin: 0; font-size: 28px;'>✂️ " + appName + "</h1>" +
                "                        </td>" +
                "                    </tr>" +
                "                    <!-- Content -->" +
                "                    <tr>" +
                "                        <td style='padding: 40px 30px;'>" +
                "                            <h2 style='color: #333333; margin: 0 0 20px 0; font-size: 24px;'>Hola " + userName + ",</h2>" +
                "                            <p style='color: #666666; font-size: 16px; line-height: 1.6; margin: 0 0 20px 0;'>" +
                "                                Recibimos una solicitud para restablecer tu contraseña. Usa el siguiente código PIN para continuar:" +
                "                            </p>" +
                "                            <!-- PIN Box -->" +
                "                            <table width='100%' cellpadding='0' cellspacing='0'>" +
                "                                <tr>" +
                "                                    <td align='center' style='padding: 20px 0;'>" +
                "                                        <div style='background-color: #f8f8f8; border: 2px dashed #6750A4; border-radius: 10px; padding: 20px; display: inline-block;'>" +
                "                                            <h1 style='color: #6750A4; margin: 0; font-size: 36px; letter-spacing: 8px; font-weight: bold;'>" + pin + "</h1>" +
                "                                        </div>" +
                "                                    </td>" +
                "                                </tr>" +
                "                            </table>" +
                "                            <p style='color: #666666; font-size: 16px; line-height: 1.6; margin: 20px 0;'>" +
                "                                Este código <strong>expirará en 15 minutos</strong> por razones de seguridad." +
                "                            </p>" +
                "                            <div style='background-color: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; border-radius: 5px;'>" +
                "                                <p style='color: #856404; margin: 0; font-size: 14px;'>" +
                "                                    ⚠️ Si no solicitaste este cambio, ignora este correo. Tu contraseña permanecerá sin cambios." +
                "                                </p>" +
                "                            </div>" +
                "                        </td>" +
                "                    </tr>" +
                "                    <!-- Footer -->" +
                "                    <tr>" +
                "                        <td style='background-color: #f8f8f8; padding: 20px 30px; text-align: center; border-top: 1px solid #eeeeee;'>" +
                "                            <p style='color: #999999; font-size: 12px; margin: 0;'>" +
                "                                © 2024 " + appName + ". Todos los derechos reservados." +
                "                            </p>" +
                "                            <p style='color: #999999; font-size: 12px; margin: 10px 0 0 0;'>" +
                "                                Este es un correo automático, por favor no respondas a este mensaje." +
                "                            </p>" +
                "                        </td>" +
                "                    </tr>" +
                "                </table>" +
                "            </td>" +
                "        </tr>" +
                "    </table>" +
                "</body>" +
                "</html>";
    }

    /**
     * Envía confirmación de cambio de contraseña exitoso
     */
    public void sendPasswordChangedConfirmation(String to, String userName) throws MessagingException {
        MimeMessage message = mailSender.createMimeMessage();
        MimeMessageHelper helper = new MimeMessageHelper(message, true, "UTF-8");

        helper.setFrom(fromEmail);
        helper.setTo(to);
        helper.setSubject("Contraseña Cambiada - " + appName);

        String htmlContent = "<!DOCTYPE html>" +
                "<html>" +
                "<body style='font-family: Arial, sans-serif;'>" +
                "    <h2>Hola " + userName + ",</h2>" +
                "    <p>Tu contraseña ha sido cambiada exitosamente.</p>" +
                "    <p>Si no realizaste este cambio, contacta con nosotros inmediatamente.</p>" +
                "    <br>" +
                "    <p>Saludos,<br>" + appName + "</p>" +
                "</body>" +
                "</html>";

        helper.setText(htmlContent, true);
        mailSender.send(message);
    }
}