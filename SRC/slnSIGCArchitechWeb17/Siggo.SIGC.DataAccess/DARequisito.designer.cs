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
	public partial class DARequisitoDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DARequisitoDataContext() : 
				base(global::Siggo.SIGC.DataAccess.Properties.Settings.Default.SiggoDataConnectionString3, mappingSource)
		{
			OnCreated();
		}
		
		public DARequisitoDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DARequisitoDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DARequisitoDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DARequisitoDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
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

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_OBTENER_LISTA_REQUISITOS")]
		public ISingleResult<SP_OBTENER_LISTA_REQUISITOSResult> SP_OBTENER_LISTA_REQUISITOS([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdEmpresa", DbType="VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TipoRecurso", DbType="VarChar(5)")] string tipoRecurso)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idEmpresa, tipoRecurso);
			return ((ISingleResult<SP_OBTENER_LISTA_REQUISITOSResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_OBTENER_DATOS_REQUISITO_DATO")]
		public ISingleResult<SP_OBTENER_DATOS_REQUISITO_DATOResult> SP_OBTENER_DATOS_REQUISITO_DATO([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdEmpresa", DbType="VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdTipoServicio", DbType="Int")] System.Nullable<int> idTipoServicio, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdTipodocumento", DbType="Int")] System.Nullable<int> idTipodocumento)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idEmpresa, idTipoServicio, idTipodocumento);
			return ((ISingleResult<SP_OBTENER_DATOS_REQUISITO_DATOResult>)(result.ReturnValue));
		}

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.SP_MANT_REG_REQUISITO_DATO")]
        public int SP_MANT_REG_REQUISITO_DATO([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Opcion", DbType = "Int")] System.Nullable<int> opcion, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdEmpresa", DbType = "VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdTipoServicio", DbType = "Int")] System.Nullable<int> idTipoServicio, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdTipoDocumento", DbType = "Int")] System.Nullable<int> idTipoDocumento, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdDato", DbType = "VarChar(5)")] string idDato, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Orden", DbType = "Int")] System.Nullable<int> orden, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RegistradoPor", DbType = "VarChar(10)")] string registradoPor, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RETURN_VALUE", DbType = "Bit")] ref System.Nullable<bool> RETURN_VALUE)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), opcion, idEmpresa, idTipoServicio, idTipoDocumento, idDato, orden, registradoPor, RETURN_VALUE);
            RETURN_VALUE = ((System.Nullable<bool>)(result.GetParameterValue(7)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.SP_MANT_REG_REQUISITO")]
        public int SP_MANT_REG_REQUISITO([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Opcion", DbType = "Int")] System.Nullable<int> opcion, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdEmpresa", DbType = "VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdTipoServicio", DbType = "Int")] System.Nullable<int> idTipoServicio, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IdTipoDocumento", DbType = "Int")] System.Nullable<int> idTipoDocumento, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TipoRecurso", DbType = "VarChar(5)")] string tipoRecurso, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DescripcionRequisito", DbType = "VarChar(200)")] string descripcionRequisito, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DiasTolerancia", DbType = "Int")] System.Nullable<int> diasTolerancia, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DiasNotificacion", DbType = "Int")] System.Nullable<int> diasNotificacion, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Periodicidad", DbType = "VarChar(20)")] string periodicidad, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HabilitaPago", DbType = "Bit")] System.Nullable<bool> habilitaPago, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HabilitaAcceso", DbType = "Bit")] System.Nullable<bool> habilitaAcceso, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DardeBaja", DbType = "Bit")] System.Nullable<bool> dardeBaja, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FechaVigenciaDesde", DbType = "DateTime")] System.Nullable<System.DateTime> fechaVigenciaDesde, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FechaVigenciaHasta", DbType = "DateTime")] System.Nullable<System.DateTime> fechaVigenciaHasta, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RegistradoPor", DbType = "VarChar(50)")] string registradoPor, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RETURN_VALUE", DbType = "Bit")] ref System.Nullable<bool> RETURN_VALUE)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), opcion, idEmpresa, idTipoServicio, idTipoDocumento, tipoRecurso, descripcionRequisito, diasTolerancia, diasNotificacion, periodicidad, habilitaPago, habilitaAcceso, dardeBaja, fechaVigenciaDesde, fechaVigenciaHasta, registradoPor, RETURN_VALUE);
            RETURN_VALUE = ((System.Nullable<bool>)(result.GetParameterValue(15)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_OBTENER_DATOS_REQUISITO")]
		public ISingleResult<SP_OBTENER_DATOS_REQUISITOResult> SP_OBTENER_DATOS_REQUISITO([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdEmpresa", DbType="VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Identificador", DbType="Int")] System.Nullable<int> identificador, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdTipoServicio", DbType="Int")] System.Nullable<int> idTipoServicio)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idEmpresa, identificador, idTipoServicio);
			return ((ISingleResult<SP_OBTENER_DATOS_REQUISITOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_LISTAR_REQUISITOS")]
		public ISingleResult<SP_LISTAR_REQUISITOSResult> SP_LISTAR_REQUISITOS([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdEmpresa", DbType="VarChar(5)")] string idEmpresa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Identificador", DbType="VarChar(5)")] string identificador, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Descripcion", DbType="VarChar(100)")] string descripcion, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TipoRecurso", DbType="VarChar(20)")] string tipoRecurso, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IDUSUARIO", DbType="VarChar(10)")] string iDUSUARIO)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idEmpresa, identificador, descripcion, tipoRecurso, iDUSUARIO);
			return ((ISingleResult<SP_LISTAR_REQUISITOSResult>)(result.ReturnValue));
		}
	}
	
	public partial class SP_OBTENER_LISTA_REQUISITOSResult
	{
		
		private int _Codigo;
		
		private string _Descripcion;
		
		private System.Nullable<int> _IdTipoServicio;
		
		public SP_OBTENER_LISTA_REQUISITOSResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Codigo", DbType="Int NOT NULL")]
		public int Codigo
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Descripcion", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdTipoServicio", DbType="Int")]
		public System.Nullable<int> IdTipoServicio
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
	}
	
	public partial class SP_OBTENER_DATOS_REQUISITO_DATOResult
	{
		
		private string _IdEmpresa;
		
		private int _IdTipoServicio;
		
		private int _IdTipoDocumento;
		
		private string _IdDato;
		
		private string _DescripcionDato;
		
		private int _Orden;
		
		private string _FECMAKER;
		
		private string _HoraRegistro;
		
		private string _MAKER;
		
		public SP_OBTENER_DATOS_REQUISITO_DATOResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdDato", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string IdDato
		{
			get
			{
				return this._IdDato;
			}
			set
			{
				if ((this._IdDato != value))
				{
					this._IdDato = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DescripcionDato", DbType="VarChar(50)")]
		public string DescripcionDato
		{
			get
			{
				return this._DescripcionDato;
			}
			set
			{
				if ((this._DescripcionDato != value))
				{
					this._DescripcionDato = value;
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
	
	public partial class SP_OBTENER_DATOS_REQUISITOResult
	{
		
		private string _IdEmpresa;
		
		private int _IdTipoServicio;
		
		private int _IdTipoDocumento;
		
		private string _TipoRecurso;
		
		private string _DescripcionRequisito;
		
		private int _DiasTolerancia;
		
		private int _DiasNotificacion;
		
		private string _Periodicidad;
		
		private bool _HabilitaPago;
		
		private bool _HabilitaAcceso;
		
		private bool _DardeBaja;
		
		private System.Nullable<System.DateTime> _FechaVigenciaDesde;
		
		private System.Nullable<System.DateTime> _FechaVigenciaHasta;
		
		private char _Estado;
		
		private char _Accion;
		
		private System.DateTime _FechaRegistro;
		
		private string _HoraRegistro;
		
		private string _RegistradoPor;
		
		private string _FECMAKER;
		
		private string _HoraRegistro1;
		
		private string _MAKER;
		
		public SP_OBTENER_DATOS_REQUISITOResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DiasTolerancia", DbType="Int NOT NULL")]
		public int DiasTolerancia
		{
			get
			{
				return this._DiasTolerancia;
			}
			set
			{
				if ((this._DiasTolerancia != value))
				{
					this._DiasTolerancia = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DiasNotificacion", DbType="Int NOT NULL")]
		public int DiasNotificacion
		{
			get
			{
				return this._DiasNotificacion;
			}
			set
			{
				if ((this._DiasNotificacion != value))
				{
					this._DiasNotificacion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Periodicidad", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string Periodicidad
		{
			get
			{
				return this._Periodicidad;
			}
			set
			{
				if ((this._Periodicidad != value))
				{
					this._Periodicidad = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HabilitaPago", DbType="Bit NOT NULL")]
		public bool HabilitaPago
		{
			get
			{
				return this._HabilitaPago;
			}
			set
			{
				if ((this._HabilitaPago != value))
				{
					this._HabilitaPago = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HabilitaAcceso", DbType="Bit NOT NULL")]
		public bool HabilitaAcceso
		{
			get
			{
				return this._HabilitaAcceso;
			}
			set
			{
				if ((this._HabilitaAcceso != value))
				{
					this._HabilitaAcceso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DardeBaja", DbType="Bit NOT NULL")]
		public bool DardeBaja
		{
			get
			{
				return this._DardeBaja;
			}
			set
			{
				if ((this._DardeBaja != value))
				{
					this._DardeBaja = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaVigenciaDesde", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaVigenciaDesde
		{
			get
			{
				return this._FechaVigenciaDesde;
			}
			set
			{
				if ((this._FechaVigenciaDesde != value))
				{
					this._FechaVigenciaDesde = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaVigenciaHasta", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaVigenciaHasta
		{
			get
			{
				return this._FechaVigenciaHasta;
			}
			set
			{
				if ((this._FechaVigenciaHasta != value))
				{
					this._FechaVigenciaHasta = value;
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
	
	public partial class SP_LISTAR_REQUISITOSResult
	{
		
		private string _IdEmpresa;
		
		private string _Contratista;
		
		private int _IdTipoDocumento;
		
		private string _DescripcionRequisito;
		
		private string _TipoRiesgo;
		
		private string _TipoRecurso;
		
		private string _DescripcionTipoRecurso;
		
		private int _IdTipoServicio;
		
		private char _ESTADO;
		
		private char _ACCION;
		
		private System.DateTime _FECHAREGISTRO;
		
		private string _HORAREGISTRO;
		
		private string _REGISTRADOPOR;
		
		public SP_LISTAR_REQUISITOSResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TipoRiesgo", DbType="VarChar(100)")]
		public string TipoRiesgo
		{
			get
			{
				return this._TipoRiesgo;
			}
			set
			{
				if ((this._TipoRiesgo != value))
				{
					this._TipoRiesgo = value;
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
}
#pragma warning restore 1591
