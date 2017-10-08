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
	public partial class DASistemaWebDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DASistemaWebDataContext() : 
				base(global::Siggo.SIGC.DataAccess.Properties.Settings.Default.SiggoDataConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public DASistemaWebDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DASistemaWebDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DASistemaWebDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DASistemaWebDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_OBTENER_OPCIONES_MENU_ROL")]
		public ISingleResult<SP_OBTENER_OPCIONES_MENU_ROLResult> SP_OBTENER_OPCIONES_MENU_ROL([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IDUSUARIO", DbType="VarChar(5)")] string iDUSUARIO)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iDUSUARIO);
			return ((ISingleResult<SP_OBTENER_OPCIONES_MENU_ROLResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_OBTENER_ROLES")]
		public ISingleResult<SP_OBTENER_ROLESResult> SP_OBTENER_ROLES([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IDEMPRESA", DbType="VarChar(10)")] string iDEMPRESA)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iDEMPRESA);
			return ((ISingleResult<SP_OBTENER_ROLESResult>)(result.ReturnValue));
		}
	}
	
	public partial class SP_OBTENER_OPCIONES_MENU_ROLResult
	{
		
		private string _IdMenu;
		
		private string _PreMenu;
		
		private string _DescripcionMenu;
		
		private char _FlagTipo;
		
		private string _Orden;
		
		private string _ActionResult;
		
		private string _Controller;
		
		private char _Estado;
		
		private char _Accion;
		
		private string _Accesos;
		
		public SP_OBTENER_OPCIONES_MENU_ROLResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdMenu", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string IdMenu
		{
			get
			{
				return this._IdMenu;
			}
			set
			{
				if ((this._IdMenu != value))
				{
					this._IdMenu = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PreMenu", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string PreMenu
		{
			get
			{
				return this._PreMenu;
			}
			set
			{
				if ((this._PreMenu != value))
				{
					this._PreMenu = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DescripcionMenu", DbType="VarChar(80) NOT NULL", CanBeNull=false)]
		public string DescripcionMenu
		{
			get
			{
				return this._DescripcionMenu;
			}
			set
			{
				if ((this._DescripcionMenu != value))
				{
					this._DescripcionMenu = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FlagTipo", DbType="Char(1) NOT NULL")]
		public char FlagTipo
		{
			get
			{
				return this._FlagTipo;
			}
			set
			{
				if ((this._FlagTipo != value))
				{
					this._FlagTipo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Orden", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string Orden
		{
			get
			{
				return this._Orden;
			}
			set
			{
				if ((this._Orden != value))
				{
					this._Orden = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActionResult", DbType="VarChar(80)")]
		public string ActionResult
		{
			get
			{
				return this._ActionResult;
			}
			set
			{
				if ((this._ActionResult != value))
				{
					this._ActionResult = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Controller", DbType="VarChar(80)")]
		public string Controller
		{
			get
			{
				return this._Controller;
			}
			set
			{
				if ((this._Controller != value))
				{
					this._Controller = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Accesos", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string Accesos
		{
			get
			{
				return this._Accesos;
			}
			set
			{
				if ((this._Accesos != value))
				{
					this._Accesos = value;
				}
			}
		}
	}
	
	public partial class SP_OBTENER_ROLESResult
	{
		
		private int _IdRol;
		
		private string _DescripcionRol;
		
		private char _Estado;
		
		private char _Accion;
		
		private string _FECMAKER;
		
		private string _HoraRegistro;
		
		private string _MAKER;
		
		public SP_OBTENER_ROLESResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdRol", DbType="Int NOT NULL")]
		public int IdRol
		{
			get
			{
				return this._IdRol;
			}
			set
			{
				if ((this._IdRol != value))
				{
					this._IdRol = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DescripcionRol", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string DescripcionRol
		{
			get
			{
				return this._DescripcionRol;
			}
			set
			{
				if ((this._DescripcionRol != value))
				{
					this._DescripcionRol = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HoraRegistro", DbType="VarChar(8) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MAKER", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
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