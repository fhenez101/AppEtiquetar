﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App.Etiketarte.Model
{
    using App.Etiketarte.Model.Common;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    
    public partial class EtiketarteEntities : DbContext
    {
        public EtiketarteEntities()
            : base("name=EtiketarteEntities")
        {
    		// Cuando un objeto es creado como parte de una consulta su estado se define como non cambiado
    		((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += (sender, args) =>
    		{
    			var entity = args.Entity as IObjectWithState;
    
    			if (entity != null)
    			{
    				entity.State = State.Unchanged;
    			}
    		};
    
    		Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
    
    	#if DEBUG
    		// https://msdn.microsoft.com/en-us/data/dn469464.aspx
    		this.Database.Log = (string data) => 
    		{
    			System.Diagnostics.Debug.WriteLine(data);
    		};	
    	#endif
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Arte> Artes { get; set; }
        public virtual DbSet<ArteForma> ArteFormas { get; set; }
        public virtual DbSet<ArteSplit> ArteSplits { get; set; }
        public virtual DbSet<CANTON_CR> CANTON_CR { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<ConfEtiketa> ConfEtiketas { get; set; }
        public virtual DbSet<DetalleColor> DetalleColors { get; set; }
        public virtual DbSet<DetalleOferta> DetalleOfertas { get; set; }
        public virtual DbSet<DetalleOrden> DetalleOrdens { get; set; }
        public virtual DbSet<DetalleTipografia> DetalleTipografias { get; set; }
        public virtual DbSet<DISTRITO_CR> DISTRITO_CR { get; set; }
        public virtual DbSet<Etiketa> Etiketas { get; set; }
        public virtual DbSet<Forma> Formas { get; set; }
        public virtual DbSet<FormaEnvio> FormaEnvios { get; set; }
        public virtual DbSet<FormaPago> FormaPagoes { get; set; }
        public virtual DbSet<FormaSplit> FormaSplits { get; set; }
        public virtual DbSet<InputText> InputTexts { get; set; }
        public virtual DbSet<Logo> Logoes { get; set; }
        public virtual DbSet<LogoEtiketa> LogoEtiketas { get; set; }
        public virtual DbSet<LogoSplit> LogoSplits { get; set; }
        public virtual DbSet<Oferta> Ofertas { get; set; }
        public virtual DbSet<Orden> Ordens { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<Presentacion> Presentacions { get; set; }
        public virtual DbSet<Producto> Productoes { get; set; }
        public virtual DbSet<PROVINCIA_CR> PROVINCIA_CR { get; set; }
        public virtual DbSet<Tipografia> Tipografias { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    	
    	#region Aplicación de cambios general
    
    	/// <summary>
    	/// Aplicación de cambios en la fuente de datos.
    	/// </summary>
    	/// <typeparam name="TEntity"></typeparam>
    	/// <param name="root"></param>
    	/// <returns></returns>
    	public void ApplyChanges<TEntity>(TEntity root) where TEntity : class, IObjectWithState
    	{
    			Set<TEntity>().Add(root);
    			CheckForEntitiesWithoutStateInterface(this);
    			foreach (var entry in ChangeTracker.Entries<IObjectWithState>())
    			{
    				IObjectWithState stateInfo = entry.Entity;
    				if (stateInfo.State == State.Modified)
    				{
    					entry.State = EntityState.Unchanged;
    					foreach (var property in stateInfo.ModifiedProperties)
    					{
    						entry.Property(property).IsModified = true;
    					}
    				}
    				else
    				{
    					entry.State = ConvertState(stateInfo.State);
    				}
    			}
    			int affected = SaveChanges();
    	}
    
    	/// <summary>
    	/// Convierte el estado entre el estado personalizado y el estado del Entity Framewok
    	/// </summary>
    	/// <param name="state"></param>
    	/// <returns></returns>
    	EntityState ConvertState(State state)
    	{
    		switch (state)
    		{
    			case State.Added:
    				return EntityState.Added;
    			case State.Modified:
    				return EntityState.Modified;
    			case State.Deleted:
    				return EntityState.Deleted;
    			default:
    				return EntityState.Unchanged;
    		}
    	}
    
    	/// <summary>
    	/// Se confirma que todos los objetos del modelo implementan la interface IObjectWithState
    	/// </summary>
    	/// <param name="context"></param>
    	void CheckForEntitiesWithoutStateInterface(EtiketarteEntities context)
    	{
    		var entitiesWithoutState =
    		from e in context.ChangeTracker.Entries()
    		where !(e.Entity is IObjectWithState)
    		select e;
    		if (entitiesWithoutState.Any())
    		{
    			throw new NotSupportedException("Todas la entidades deben implementar IObjectWithState.");
    		}
    	}
    	#endregion
    
    }
}
