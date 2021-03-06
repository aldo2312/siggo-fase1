﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Siggo.SIGC.DataAccess
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SiggoData")]
	public partial class DAMaestroDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DAMaestroDataContext() : 
				base(global::Siggo.SIGC.DataAccess.Properties.Settings.Default.SiggoDataConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public DAMaestroDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DAMaestroDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DAMaestroDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DAMaestroDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_OBTENER_LISTA_MAESTRO")]
		public ISingleResult<SP_OBTENER_LISTA_MAESTROResult> SP_OBTENER_LISTA_MAESTRO([global::System.Data.Linq.Mapping.ParameterAttribute(Name="CodigoTabla", DbType="VarChar(10)")] string codigoTabla)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), codigoTabla);
			return ((ISingleResult<SP_OBTENER_LISTA_MAESTROResult>)(result.ReturnValue));
		}
	}
	
	public partial class SP_OBTENER_LISTA_MAESTROResult
	{
		
		private string _CodigoTabla;
		
		private string _NombreTabla;
		
		private string _Codigo;
		
		private string _Descripcion;
		
		private string _Valor;
		
		private char _Estado;
		
		private char _Accion;
		
		private string _FECMAKER;
		
		private string _HoraRegistro;
		
		private string _MAKER;
		
		public SP_OBTENER_LISTA_MAESTROResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodigoTabla", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string CodigoTabla
		{
			get
			{
				return this._CodigoTabla;
			}
			set
			{
				if ((this._CodigoTabla != value))
				{
					this._CodigoTabla = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NombreTabla", DbType="VarChar(25) NOT NULL", CanBeNull=false)]
		public string NombreTabla
		{
			get
			{
				return this._NombreTabla;
			}
			set
			{
				if ((this._NombreTabla != value))
				{
					this._NombreTabla = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Codigo", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string Codigo
		{
			get
			{
				return this._Codigo;
			}
			set
			{
				if ((this._Codigo != value))
				{
					this._Codigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Descripcion", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Descripcion
		{
			get
			{
				return this._Descripcion;
			}
			set
			{
				if ((this._Descripcion != value))
				{
					this._Descripcion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Valor", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Valor
		{
			get
			{
				return this._Valor;
			}
			set
			{
				if ((this._Valor != value))
				{
					this._Valor = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estado", DbType="Char(1) NOT NULL")]
		public char Estado
		{
			get
			{
				return this._Estado;
			}
			set
			{
				if ((this._Estado != value))
				{
					this._Estado = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Accion", DbType="Char(1) NOT NULL")]
		public char Accion
		{
			get
			{
				return this._Accion;
			}
			set
			{
				if ((this._Accion != value))
				{
					this._Accion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FECMAKER", DbType="VarChar(30)")]
		public string FECMAKER
		{
			get
			{
				return this._FECMAKER;
			}
			set
			{
				if ((this._FECMAKER != value))
				{
					this._FECMAKER = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HoraRegistro", DbType="Char(8) NOT NULL", CanBeNull=false)]
		public string HoraRegistro
		{
			get
			{
				return this._HoraRegistro;
			}
			set
			{
				if ((this._HoraRegistro != value))
				{
					this._HoraRegistro = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MAKER", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string MAKER
		{
			get
			{
				return this._MAKER;
			}
			set
			{
				if ((this._MAKER != value))
				{
					this._MAKER = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
