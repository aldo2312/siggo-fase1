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
	public partial class DARecursoDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DARecursoDataContext() : 
				base(global::Siggo.SIGC.DataAccess.Properties.Settings.Default.SiggoDataConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public DARecursoDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DARecursoDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DARecursoDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DARecursoDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.SP_MANT_REG_REQUISITO_RECURSO")]
        public int SP_MANT_REG_REQUISITO_RECURSO([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Opcion", DbType = "Int")] System.Nullable<int> opcion, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdEmpresa", DbType = "VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdRecurso", DbType = "VarChar(5)")] string idRecurso, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdTipoServicio", DbType = "Int")] System.Nullable<int> idTipoServicio, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdTipoDocumento", DbType = "Int")] System.Nullable<int> idTipoDocumento, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EsRequerido", DbType = "Bit")] System.Nullable<bool> esRequerido, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NecesitaAdjunto", DbType = "Bit")] System.Nullable<bool> necesitaAdjunto, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Orden", DbType = "Int")] System.Nullable<int> orden, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RegistradoPor", DbType = "VarChar(50)")] string registradoPor, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RETURN_VALUE", DbType = "Bit")] ref System.Nullable<bool> RETURN_VALUE)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), opcion, idEmpresa, idRecurso, idTipoServicio, idTipoDocumento, esRequerido, necesitaAdjunto, orden, registradoPor, RETURN_VALUE);
            RETURN_VALUE = ((System.Nullable<bool>)(result.GetParameterValue(9)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_LISTAR_RECURSOS")]
		public ISingleResult<SP_LISTAR_RECURSOSResult> SP_LISTAR_RECURSOS([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdEmpresa", DbType="VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Identificador", DbType="VarChar(5)")] string identificador, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Descripcion", DbType="VarChar(100)")] string descripcion, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TipoRecurso", DbType="VarChar(20)")] string tipoRecurso)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idEmpresa, identificador, descripcion, tipoRecurso);
			return ((ISingleResult<SP_LISTAR_RECURSOSResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_OBTENER_DATOS_RECURSO")]
		public ISingleResult<SP_OBTENER_DATOS_RECURSOResult> SP_OBTENER_DATOS_RECURSO([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdEmpresa", DbType="VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Identificador", DbType="VarChar(5)")] string identificador)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idEmpresa, identificador);
			return ((ISingleResult<SP_OBTENER_DATOS_RECURSOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_MANT_REG_RECURSO")]
		public int SP_MANT_REG_RECURSO([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Opcion", DbType="Int")] System.Nullable<int> opcion, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdEmpresa", DbType="VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdRecurso", DbType="VarChar(5)")] string idRecurso, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="NroReferencia", DbType="VarChar(20)")] string nroReferencia, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Descripcion1", DbType="VarChar(200)")] string descripcion1, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Descripcion2", DbType="VarChar(200)")] string descripcion2, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Descripcion3", DbType="VarChar(200)")] string descripcion3, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Descripcion4", DbType="VarChar(200)")] string descripcion4, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Observacion", DbType="VarChar(500)")] string observacion, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Cantidad", DbType="Int")] System.Nullable<int> cantidad, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TipoRecurso", DbType="VarChar(5)")] string tipoRecurso, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="RegistradoPor", DbType="VarChar(50)")] string registradoPor, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RETURN_VALUE", DbType = "Bit")] ref System.Nullable<bool> RETURN_VALUE)
        {
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), opcion, idEmpresa, idRecurso, nroReferencia, descripcion1, descripcion2, descripcion3, descripcion4, observacion, cantidad, tipoRecurso, registradoPor, RETURN_VALUE);
            RETURN_VALUE = ((System.Nullable<bool>)(result.GetParameterValue(12)));
            return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_OBTENER_DATOS_REQUISITO_RECURSO")]
		public ISingleResult<SP_OBTENER_DATOS_REQUISITO_RECURSOResult> SP_OBTENER_DATOS_REQUISITO_RECURSO([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdEmpresa", DbType="VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdRecurso", DbType="VarChar(5)")] string idRecurso)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idEmpresa, idRecurso);
			return ((ISingleResult<SP_OBTENER_DATOS_REQUISITO_RECURSOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_OBTENER_DATOS_REQUISITO_SERVICIO")]
		public ISingleResult<SP_OBTENER_DATOS_REQUISITO_SERVICIOResult> SP_OBTENER_DATOS_REQUISITO_SERVICIO([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdEmpresa", DbType="VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdTipoServicio", DbType="Int")] System.Nullable<int> idTipoServicio, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TipoRecurso", DbType="VarChar(20)")] string tipoRecurso)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idEmpresa, idTipoServicio, tipoRecurso);
			return ((ISingleResult<SP_OBTENER_DATOS_REQUISITO_SERVICIOResult>)(result.ReturnValue));
		}
	}
	
	public partial class SP_LISTAR_RECURSOSResult
	{
		
		private string _IdEmpresa;
		
		private string _Contratista;
		
		private string _IdRecurso;
		
		private string _NroReferencia;
		
		private string _DescripcionRecurso;
		
		private string _DescripcionLarga;
		
		private string _TipoRecurso;
		
		private string _DescripcionTipoRecurso;
		
		private char _ESTADO;
		
		private char _ACCION;
		
		private System.DateTime _FECHAREGISTRO;
		
		private string _HORAREGISTRO;
		
		private string _REGISTRADOPOR;
		
		public SP_LISTAR_RECURSOSResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEmpresa", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string IdEmpresa
		{
			get
			{
				return this._IdEmpresa;
			}
			set
			{
				if ((this._IdEmpresa != value))
				{
					this._IdEmpresa = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Contratista", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Contratista
		{
			get
			{
				return this._Contratista;
			}
			set
			{
				if ((this._Contratista != value))
				{
					this._Contratista = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdRecurso", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string IdRecurso
		{
			get
			{
				return this._IdRecurso;
			}
			set
			{
				if ((this._IdRecurso != value))
				{
					this._IdRecurso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NroReferencia", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string NroReferencia
		{
			get
			{
				return this._NroReferencia;
			}
			set
			{
				if ((this._NroReferencia != value))
				{
					this._NroReferencia = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DescripcionRecurso", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string DescripcionRecurso
		{
			get
			{
				return this._DescripcionRecurso;
			}
			set
			{
				if ((this._DescripcionRecurso != value))
				{
					this._DescripcionRecurso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DescripcionLarga", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
		public string DescripcionLarga
		{
			get
			{
				return this._DescripcionLarga;
			}
			set
			{
				if ((this._DescripcionLarga != value))
				{
					this._DescripcionLarga = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TipoRecurso", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string TipoRecurso
		{
			get
			{
				return this._TipoRecurso;
			}
			set
			{
				if ((this._TipoRecurso != value))
				{
					this._TipoRecurso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DescripcionTipoRecurso", DbType="VarChar(100)")]
		public string DescripcionTipoRecurso
		{
			get
			{
				return this._DescripcionTipoRecurso;
			}
			set
			{
				if ((this._DescripcionTipoRecurso != value))
				{
					this._DescripcionTipoRecurso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ESTADO", DbType="Char(1) NOT NULL")]
		public char ESTADO
		{
			get
			{
				return this._ESTADO;
			}
			set
			{
				if ((this._ESTADO != value))
				{
					this._ESTADO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ACCION", DbType="Char(1) NOT NULL")]
		public char ACCION
		{
			get
			{
				return this._ACCION;
			}
			set
			{
				if ((this._ACCION != value))
				{
					this._ACCION = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FECHAREGISTRO", DbType="DateTime NOT NULL")]
		public System.DateTime FECHAREGISTRO
		{
			get
			{
				return this._FECHAREGISTRO;
			}
			set
			{
				if ((this._FECHAREGISTRO != value))
				{
					this._FECHAREGISTRO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HORAREGISTRO", DbType="Char(8) NOT NULL", CanBeNull=false)]
		public string HORAREGISTRO
		{
			get
			{
				return this._HORAREGISTRO;
			}
			set
			{
				if ((this._HORAREGISTRO != value))
				{
					this._HORAREGISTRO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_REGISTRADOPOR", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string REGISTRADOPOR
		{
			get
			{
				return this._REGISTRADOPOR;
			}
			set
			{
				if ((this._REGISTRADOPOR != value))
				{
					this._REGISTRADOPOR = value;
				}
			}
		}
	}
	
	public partial class SP_OBTENER_DATOS_RECURSOResult
	{
		
		private string _IdEmpresa;
		
		private string _IdRecurso;
		
		private string _NroReferencia;
		
		private string _Descripcion1;
		
		private string _Descripcion2;
		
		private string _Descripcion3;
		
		private string _Descripcion4;
		
		private string _Observacion;
		
		private int _Cantidad;
		
		private string _TipoRecurso;
		
		private char _Estado;
		
		private char _Accion;
		
		private System.DateTime _FechaRegistro;
		
		private string _HoraRegistro;
		
		private string _RegistradoPor;
		
		private string _FECMAKER;
		
		private string _HoraRegistro1;
		
		private string _MAKER;
		
		public SP_OBTENER_DATOS_RECURSOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEmpresa", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string IdEmpresa
		{
			get
			{
				return this._IdEmpresa;
			}
			set
			{
				if ((this._IdEmpresa != value))
				{
					this._IdEmpresa = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdRecurso", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string IdRecurso
		{
			get
			{
				return this._IdRecurso;
			}
			set
			{
				if ((this._IdRecurso != value))
				{
					this._IdRecurso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NroReferencia", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string NroReferencia
		{
			get
			{
				return this._NroReferencia;
			}
			set
			{
				if ((this._NroReferencia != value))
				{
					this._NroReferencia = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Descripcion1", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string Descripcion1
		{
			get
			{
				return this._Descripcion1;
			}
			set
			{
				if ((this._Descripcion1 != value))
				{
					this._Descripcion1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Descripcion2", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string Descripcion2
		{
			get
			{
				return this._Descripcion2;
			}
			set
			{
				if ((this._Descripcion2 != value))
				{
					this._Descripcion2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Descripcion3", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string Descripcion3
		{
			get
			{
				return this._Descripcion3;
			}
			set
			{
				if ((this._Descripcion3 != value))
				{
					this._Descripcion3 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Descripcion4", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string Descripcion4
		{
			get
			{
				return this._Descripcion4;
			}
			set
			{
				if ((this._Descripcion4 != value))
				{
					this._Descripcion4 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Observacion", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
		public string Observacion
		{
			get
			{
				return this._Observacion;
			}
			set
			{
				if ((this._Observacion != value))
				{
					this._Observacion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Cantidad", DbType="Int NOT NULL")]
		public int Cantidad
		{
			get
			{
				return this._Cantidad;
			}
			set
			{
				if ((this._Cantidad != value))
				{
					this._Cantidad = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TipoRecurso", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string TipoRecurso
		{
			get
			{
				return this._TipoRecurso;
			}
			set
			{
				if ((this._TipoRecurso != value))
				{
					this._TipoRecurso = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaRegistro", DbType="DateTime NOT NULL")]
		public System.DateTime FechaRegistro
		{
			get
			{
				return this._FechaRegistro;
			}
			set
			{
				if ((this._FechaRegistro != value))
				{
					this._FechaRegistro = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RegistradoPor", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string RegistradoPor
		{
			get
			{
				return this._RegistradoPor;
			}
			set
			{
				if ((this._RegistradoPor != value))
				{
					this._RegistradoPor = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HoraRegistro1", DbType="Char(8) NOT NULL", CanBeNull=false)]
		public string HoraRegistro1
		{
			get
			{
				return this._HoraRegistro1;
			}
			set
			{
				if ((this._HoraRegistro1 != value))
				{
					this._HoraRegistro1 = value;
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
	
	public partial class SP_OBTENER_DATOS_REQUISITO_RECURSOResult
	{
		
		private string _IdEmpresa;
		
		private string _IdRecurso;
		
		private int _IdTipoServicio;
		
		private int _IdTipoDocumento;
		
		private string _DescripcionRequisito;
		
		private bool _EsRequerido;
		
		private bool _NecesitaAdjunto;
		
		private int _Orden;
		
		private char _Estado;
		
		private char _Accion;
		
		private string _FECMAKER;
		
		private string _HoraRegistro;
		
		private string _MAKER;
		
		public SP_OBTENER_DATOS_REQUISITO_RECURSOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEmpresa", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string IdEmpresa
		{
			get
			{
				return this._IdEmpresa;
			}
			set
			{
				if ((this._IdEmpresa != value))
				{
					this._IdEmpresa = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdRecurso", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string IdRecurso
		{
			get
			{
				return this._IdRecurso;
			}
			set
			{
				if ((this._IdRecurso != value))
				{
					this._IdRecurso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdTipoServicio", DbType="Int NOT NULL")]
		public int IdTipoServicio
		{
			get
			{
				return this._IdTipoServicio;
			}
			set
			{
				if ((this._IdTipoServicio != value))
				{
					this._IdTipoServicio = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdTipoDocumento", DbType="Int NOT NULL")]
		public int IdTipoDocumento
		{
			get
			{
				return this._IdTipoDocumento;
			}
			set
			{
				if ((this._IdTipoDocumento != value))
				{
					this._IdTipoDocumento = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DescripcionRequisito", DbType="VarChar(200)")]
		public string DescripcionRequisito
		{
			get
			{
				return this._DescripcionRequisito;
			}
			set
			{
				if ((this._DescripcionRequisito != value))
				{
					this._DescripcionRequisito = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EsRequerido", DbType="Bit NOT NULL")]
		public bool EsRequerido
		{
			get
			{
				return this._EsRequerido;
			}
			set
			{
				if ((this._EsRequerido != value))
				{
					this._EsRequerido = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NecesitaAdjunto", DbType="Bit NOT NULL")]
		public bool NecesitaAdjunto
		{
			get
			{
				return this._NecesitaAdjunto;
			}
			set
			{
				if ((this._NecesitaAdjunto != value))
				{
					this._NecesitaAdjunto = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Orden", DbType="Int NOT NULL")]
		public int Orden
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HoraRegistro", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MAKER", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
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
	
	public partial class SP_OBTENER_DATOS_REQUISITO_SERVICIOResult
	{
		
		private int _IdTipoDocumento;
		
		private string _DescripcionRequisito;
		
		public SP_OBTENER_DATOS_REQUISITO_SERVICIOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdTipoDocumento", DbType="Int NOT NULL")]
		public int IdTipoDocumento
		{
			get
			{
				return this._IdTipoDocumento;
			}
			set
			{
				if ((this._IdTipoDocumento != value))
				{
					this._IdTipoDocumento = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DescripcionRequisito", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string DescripcionRequisito
		{
			get
			{
				return this._DescripcionRequisito;
			}
			set
			{
				if ((this._DescripcionRequisito != value))
				{
					this._DescripcionRequisito = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
