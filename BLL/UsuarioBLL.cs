using ProyectoFinalAP2.DAL;
using ProyectoFinalAP2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAP2.BLL
{
    public class UsuarioBLL
    {
        public static bool Guardar(Usuario usuario)
        {
            if (!Existe(usuario.UsuarioId))
                return Insertar(usuario);
            else
                return Modificar(usuario);
        }

        private static bool Insertar(Usuario usuario)
        {
            if (usuario.UsuarioId != 0)
                return false;

            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                usuario.Contrasena = Encriptar(usuario.Contrasena);

                if (contexto.Usuarios.Add(usuario) != null)
                    paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        private static bool Modificar(Usuario usuario)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                usuario.Contrasena = Encriptar(usuario.Contrasena);

                contexto.Entry(usuario).State = EntityState.Modified;
                paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static string Encriptar(string cadenaEncriptada)
        {
            if (!string.IsNullOrEmpty(cadenaEncriptada))
            {
                string resultado = string.Empty;
                byte[] encryted = Encoding.Unicode.GetBytes(cadenaEncriptada);
                resultado = Convert.ToBase64String(encryted);

                return resultado;
            }
            return string.Empty;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var usuario = contexto.Usuarios.Find(id);
                if (usuario != null)
                {
                    contexto.Usuarios.Remove(usuario);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Usuario Buscar(int id)
        {
            Usuario usuario = new Usuario();
            Contexto contexto = new Contexto();
            try
            {
                usuario = contexto.Usuarios.Find(id);
                if (usuario != null)
                    usuario.Contrasena = DesEncriptar(usuario.Contrasena);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return usuario;
        }

        public static string DesEncriptar(string cadenaDesencriptada)
        {
            if (!string.IsNullOrEmpty(cadenaDesencriptada))
            {
                string resultado = string.Empty;
                byte[] decryted = Convert.FromBase64String(cadenaDesencriptada);
                resultado = System.Text.Encoding.Unicode.GetString(decryted);

                return resultado;
            }
            return string.Empty;
        }

        private static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto contexto = new Contexto();

            try
            {
                encontrado = contexto.Usuarios.Any(u => u.UsuarioId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static List<Usuario> GetList(Expression<Func<Usuario, bool>> usuario)
        {
            List<Usuario> Lista = new List<Usuario>();
            Contexto contexto = new Contexto();
            try
            {
                Lista = contexto.Usuarios.Where(usuario).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }

        public static bool ExisteUsuario(string usuario, string clave)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                clave = Encriptar(clave);

                if (contexto.Usuarios.Where(u => u.NombreUsuario == usuario && u.Contrasena == clave).SingleOrDefault() != null)
                    paso = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static string ObtenerUsuarioId(string usuario, string clave)
        {
            Contexto contexto = new Contexto();
            string id;

            try
            {
                clave = Encriptar(clave);

                id = contexto.Usuarios.Where(u => u.NombreUsuario == usuario && u.Contrasena == clave).FirstOrDefault().UsuarioId.ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return id;
        }
    }
}
