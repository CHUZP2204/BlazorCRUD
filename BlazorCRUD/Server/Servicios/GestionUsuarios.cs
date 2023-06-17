﻿using BlazorCRUD.Server.Servicios;

using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlazorCRUD.Server.Modelos
{
    public class GestionUsuarios : IUsuario
    {
        readonly PruebablazorContext dbContext = new();

        public GestionUsuarios(PruebablazorContext db)
        {
            dbContext = db;
        }

        public void ActualizaUsuario(Usuario u)
        {
            try
            {
                dbContext.Entry(u).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void BorrarUsuario(int id)
        {
            string prueba = Directory.GetCurrentDirectory();
            try
            {
                Usuario? u = dbContext.Usuarios.Find(id);
                if (u !=null)
                {
                    dbContext.Usuarios.Remove(u); 
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public Usuario DatosUsuario(int id)
        {
            try
            {
                Usuario? u = dbContext.Usuarios.Find(id);
                if (u != null)
                {
                    return u;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public List<Usuario> DatosUsuarios()
        {
            try
            {
                return dbContext.Usuarios.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void NuevoUsuario(Usuario u)
        {
            try
            {
                u.FechaAlta = DateTime.Now;
                dbContext.Usuarios.Add(u);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
