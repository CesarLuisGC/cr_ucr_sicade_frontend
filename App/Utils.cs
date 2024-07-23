using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Entities.Models.Base;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App
{
    public class Utils(IConfiguration _configuration)
    {
        #region Properties
        private readonly IConfiguration configuration = _configuration;

        #endregion

        #region Configuration Methods
        public string ConnectionString(string name)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(configuration.GetConnectionString(name)));
        }

        public string GetConfiguration(string name)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(configuration[name]));
        }

        public string GetConfigurationBase64(string name)
        {
            return configuration[name];
        }
        #endregion

        #region Static Methods

        #region Encryption
        public static string? EncodeBase64(string text)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            }
            catch
            {
                return null;
            }
        }

        public static string? DecodeBase64(string text)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(text));
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Serialization
        public static T? Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }
        #endregion

        #region Exceptions
        public static Exception BuildException(string? mensaje = null, string? function = null, string? file = null, params string[] details)
        {
            string log = $"Fecha: [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}];";
            log += $"Mensaje: [{mensaje}];";

            if (!(function is null))
            {
                log += $"Funcion: [{function}];";
            }

            if (!(file is null))
            {
                log += $"Clase: [{file}];";
            }

            if (details.Length != 0)
            {
                log += $"Detalles: [";

                foreach (var item in details)
                {
                    log += item + "|";
                }

                log += "];";
            }

            return new Exception(log);
        }

        public static string BuildExceptionMessage(Exception ex)
        {
            try
            {
                string[] mensaje = ex.Message.Split(';');

                return mensaje[1].Replace("Mensaje: [", "").Replace("]", "");
            }
            catch
            {
                return "¡Error interno del sistema, contacte a soporte!";
            }
        }

        public static Exception ThrowException(string proceso, string origen, string mensaje)
        {
            string exception = "Fecha: [@pFecha]; Proceso: [@pProceso]; Origen: [@pOrigen]; Mensaje: [@pMensaje]";

            exception = exception.Replace("@pFecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            exception = exception.Replace("@pProceso", proceso);
            exception = exception.Replace("@pOrigen", origen);
            exception = exception.Replace("@pMensaje", mensaje);

            return new Exception(exception);
        }
        #endregion

        #region Auth
        public static Token BuildToken(object session, string iss, byte[] key, short time)
        {
            try
            {
                List<Claim> claims = new() {
                    new Claim("session", JsonConvert.SerializeObject(session)),
                };

                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
                SigningCredentials creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                DateTime expiration = DateTime.Now.AddHours(time);

                JwtSecurityToken token = new JwtSecurityToken
                (
                    issuer: iss,
                    audience: null,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds
                );

                return new Token()
                {
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = expiration,
                };
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un error generando el token");
            }
        }

        public static T CurrentSession<T>(ClaimsPrincipal claims)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(claims.FindFirst("session")!.Value!)!;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error obteniendo la información del usuario actual.", ex);
            }
        }
        #endregion

        #region Views
        public static IActionResult View(ControllerBase controller, params string[] path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", Path.Combine(path));

            var fileContent = File.ReadAllText(filePath);

            return controller.Content(fileContent, "text/html");
        }
        #endregion

        #region ConnectionStrings
        public static string ConnectionString(IConfiguration _configuration, string name)
        {
            string connectionString = _configuration.GetConnectionString(name) ?? throw new Exception("The connection string do not exists");

            string decoded = DecodeBase64(connectionString) ?? throw new Exception("The connections string do not have the correct format");

            return decoded;
        }
        #endregion

        #endregion
    }
}