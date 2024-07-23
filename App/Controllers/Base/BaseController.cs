using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Entities.Models.Base;

namespace App.Controllers.Base
{
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        protected T _manager;

        protected BaseController(T _manager)
        {
            this._manager = _manager;
        }

        protected IActionResult OkBase(object data)
        {
            return base.Ok(data);
        }

        protected new IActionResult Ok()
        {
            return base.Ok(new Response()
            {
                state = 2,
                message = "¡Proceso exitoso!",
                data = null
            });
        }

        protected new IActionResult Ok(object data)
        {
            return base.Ok(new Response()
            {
                state = 2,
                message = "¡Proceso exitoso!",
                data = data
            });
        }

        protected IActionResult Ok(string msj)
        {
            return base.Ok(new Response()
            {
                state = 2,
                message = msj,
                data = null
            });
        }

        protected IActionResult Ok(object data, string msj)
        {
            return base.Ok(new Response()
            {
                state = 2,
                message = msj,
                data = data
            });
        }

        public JwtSecurityToken VerifyToken(string token, byte[] key)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            
            if (string.IsNullOrEmpty(token)) throw new Exception("No se ha enviado el token");

            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token.Replace("Bearer ", ""), new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    //ValidIssuer = Configuration["Jwt:issuer"], REVISAR PORQUE FALLA (POSIBLEMENTE ERROR DE .NETCORE 5)
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch
            {
                throw new Exception("El token enviado no es valido");
            }
        }

        protected JwtSecurityToken VerifyAuthorization(IHeaderDictionary headers, byte[] key)
        {
            try
            {
                if (!headers.ContainsKey("Authorization") || string.IsNullOrEmpty(headers["Authorization"]))
                    throw new Exception("Esta ruta necesita autorización");

                string token = headers["Authorization"];

                if (!token.Contains("Bearer "))
                    throw new Exception("Es necesario enviar el esquema de autenticación correspondiente");

                if (string.IsNullOrEmpty(token))
                    throw new Exception("Es requisito enviar el token de autenticación");

                return VerifyToken(token, key);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected P GetParam<P>(string pValue)
        {
            Type paramType = typeof(P);

            if (paramType.Equals(typeof(short)) || paramType.Equals(typeof(int)) || paramType.Equals(typeof(long)))
            {
                pValue = Utils.DecodeBase64(pValue) ?? "-1";
            }

            if (paramType.Equals(typeof(decimal)) || paramType.Equals(typeof(double)) || paramType.Equals(typeof(float)))
            {
                pValue = Utils.DecodeBase64(pValue) ?? "-1.00";
            }

            if (paramType.Equals(typeof(DateTime)))
            {
                pValue = Utils.DecodeBase64(pValue) ?? "1900-01-01";
            }

            if (paramType.Equals(typeof(string)))
            {
                pValue = Utils.DecodeBase64(pValue) ?? "";
            }

            if (paramType.Equals(typeof(bool)))
            {
                pValue = Utils.DecodeBase64(pValue) ?? "false";
            }

            return (P)Convert.ChangeType(pValue, paramType);
        }

        protected P GetObject<P>(JsonElement element, string properlyName)
        {
            Type paramType = typeof(P);

            if (!element.TryGetProperty(properlyName, out JsonElement value))
            {
                throw new Exception(string.Format("Mensaje: [SHOW: La propiedad {0} no existe en el body]", properlyName));
            }

            return IsPrimitive<P>() ? (P)Convert.ChangeType(value.ToString(), paramType) : JsonConvert.DeserializeObject<P>(value.ToString());
        }

        protected bool IsPrimitive<P>()
        {
            Type paramType = typeof(P);
            Boolean isPrimitive = false;

            if (paramType.Equals(typeof(short)) || paramType.Equals(typeof(int)) || paramType.Equals(typeof(long)))
            {
                isPrimitive = true;
            }

            if (paramType.Equals(typeof(decimal)) || paramType.Equals(typeof(double)) || paramType.Equals(typeof(float)))
            {
                isPrimitive = true;
            }

            if (paramType.Equals(typeof(DateTime)))
            {
                isPrimitive = true;
            }

            if (paramType.Equals(typeof(string)))
            {
                isPrimitive = true;
            }

            if (paramType.Equals(typeof(bool)))
            {
                isPrimitive = true;
            }

            return isPrimitive;
        }

        protected Exception ThrowException(string proceso, string origen, string mensaje)
        {
            return Utils.ThrowException(proceso, origen, mensaje);
        }
    }
}
