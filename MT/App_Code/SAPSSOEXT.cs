#define x64 // platform target on x64 (64 bit)
//#define x86  // platform target on x86 (32 bit)

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security;
using System.Threading;
using SAPSSOEXT;

namespace sapssoext
{
        public struct TICKET_INFO
        {
                public string sapUser;
                public string sapSysID;
                public string sapClient;
                public byte[] sapCert;
        }

        public struct TICKET_PORTAL_INFO
        {
                public string sapUser;
                public string sapPortalUser;
                public string sapSysID;
                public string sapClient;
                public byte[] sapCert;
        }

        public struct TICKET_PORTAL_INFO_LONG
        {
                public string sapUser;
                public string sapPortalUser;
                public string sapSysID;
                public string sapClient;
                public string sapAuthSchema;
                public Int64  validity;
                public byte[] sapCert;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct TICKET_INFO_LONG
        {
#if x86
                [MarshalAs(UnmanagedType.U4)]
                public UInt32 struct_size;
#else
                [MarshalAs(UnmanagedType.U8)]
                public UInt64 struct_size;
#endif
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
                public byte[] sSAPCodepage;
                [MarshalAs(UnmanagedType.LPStr, SizeConst = 8)]
                public string pSysID;
                [MarshalAs(UnmanagedType.SysInt)]
                public IntPtr pIntSysIDL;
                [MarshalAs(UnmanagedType.LPStr, SizeConst = 12)]
                public string pSAPUser;
                [MarshalAs(UnmanagedType.SysInt)]
                public IntPtr pIntSAPUserL;
                [MarshalAs(UnmanagedType.LPStr, SizeConst = 3)]
                public string pSAPClient;
                [MarshalAs(UnmanagedType.SysInt)]
                public IntPtr pIntSAPClientL;
                [MarshalAs(UnmanagedType.LPStr, SizeConst = 2)]
                public string pSAPLangu;
                [MarshalAs(UnmanagedType.SysInt)]
                public IntPtr pIntSAPLanguL;
                [MarshalAs(UnmanagedType.SysInt)]
                public IntPtr ptmCreTime;
                [MarshalAs(UnmanagedType.SysInt)]
                public IntPtr ptmValidity;
                [MarshalAs(UnmanagedType.LPStr, SizeConst = 256)]
                public string pPrtUser;
                [MarshalAs(UnmanagedType.SysInt)]
                public IntPtr pIntPrtUserL;
                [MarshalAs(UnmanagedType.LPStr, SizeConst = 256)]
                public string pAuthScheme;
                [MarshalAs(UnmanagedType.SysInt)]
                public IntPtr pIntAuthSchemeL;
        }

        [SuppressUnmanagedCodeSecurity()]
        internal class NativeMethods
        {
                /********************* process global methods ******************************/
                /************** INVOKE THESE METHODS ONLY ONCE PER PROCESS *****************/
                /* initialize SAPSSOEXT and SSF library */
                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32 MySapInitialize(String pszSsfLib);

                /* shutdown SAPSSEXT and SSF library */
                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32 MySapShutdown();
                
                /* get version of SAPSSOEXT */
                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32 MySapGetVersion();
                
                /* load a key file into the memory, you can omit then pszAddrBook and pszPassword */
                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32 MySapLoadTicketKey(   byte[] pKey,
                                                                   Int32  pKeyLen,
                                                                   String pszPassword,
                                                                   Int32  iIndex,
                                                                   Int32  tType );                
                /************** INVOKE THESE METHODS ONLY ONCE PER PROCESS *****************/
                /********************* process global methods ******************************/

                /* set property for SAPSSOEXT runtime */
                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32 SsoExtSetProperty(String pParameter, String pValue);

                /* get property from SAPSSOEXT runtime, if Null, then retrieve a list of all properties */
                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern String SsoExtGetProperty(String pParameter);

                /********************* thread local methods *********************************/
                /* create a SAP assertion ticket */
                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32 MySapCreateAssertionTicket(String pszMySysId,
                                                                     String pszMySysClient,
                                                                     String pszAdrBook,
                                                                     String pszPassword,
                                                       ref TICKET_INFO_LONG ticket_info,
                                  [MarshalAs(UnmanagedType.LPArray)] byte[] pTicketBuffer,
                               [MarshalAs(UnmanagedType.LPArray)]   Int32[] pTicketLen);

                /* validate a ticket */
                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32 MySapEvalLogonTicket(String pszTicket,
                                                                     String pszAdrBook,
                                                                     String pszPassword,
                                [MarshalAs(UnmanagedType.LPArray)]   byte[] putf8User,
                              [MarshalAs(UnmanagedType.LPArray)]   UInt32[] putf8UserLen,
                                [MarshalAs(UnmanagedType.LPArray)]   byte[] pASN1Cert,
                              [MarshalAs(UnmanagedType.LPArray)]   UInt32[] pASN1CertLen,
                                [MarshalAs(UnmanagedType.LPArray)]   byte[] putf8SysID,
                              [MarshalAs(UnmanagedType.LPArray)]   UInt32[] putf8SysIDLen,
                                [MarshalAs(UnmanagedType.LPArray)]   byte[] putf8Client,
                              [MarshalAs(UnmanagedType.LPArray)]   UInt32[] putf8ClientLen,
                                                                     IntPtr pvReserverd);

                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32  MySapEvalLogonTicketEx(String pszTicket,
                                                                     String pszAdrBook,
                                                                     String pszPassword,
                                [MarshalAs(UnmanagedType.LPArray)]   byte[] pASN1Cert,
                              [MarshalAs(UnmanagedType.LPArray)]   UInt32[] pASN1CertLen,
                                                       ref TICKET_INFO_LONG ticket_info);

                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32 MySapEvalAssertionTicket(String pszTicket,
                                                                     String pszAdrBook,
                                                                     String pszPassword,
                                                                     String pszMySysId,
                                                                     String pszMySysClient,
                                                       ref TICKET_INFO_LONG ticket_info);

                /* parse certificate information */
                [DllImport("sapssoext", CharSet = CharSet.Ansi, SetLastError = true)]
                internal static extern Int32 MySapParseCertificate(byte[] pASN1Cert,
                                                                   UInt32 pASN1CertLen,
                                                                   String pszType,
                                [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
                             [MarshalAs(UnmanagedType.LPArray)]   Int32[] pBufferLen);                                                                 
                /********************* thread local methods *********************************/
        }

        public class SAPSSOEXT
        {              
                private TICKET_INFO_LONG m_ticket_info = new TICKET_INFO_LONG();

                public SAPSSOEXT()
                {
                        /* init */
                        initLongInfo();
                }

                public SAPSSOEXT(String ssfLibrary)
                {
                        initLongInfo();
                        StartSSOExt(ssfLibrary);
                }

                // finalize
                ~SAPSSOEXT( )
                {
                        if (m_ticket_info.pIntAuthSchemeL == IntPtr.Zero) 
                                Marshal.FreeHGlobal(m_ticket_info.pIntAuthSchemeL);
                        m_ticket_info.pIntAuthSchemeL = IntPtr.Zero;
                        if (m_ticket_info.pIntPrtUserL == IntPtr.Zero)
                                Marshal.FreeHGlobal(m_ticket_info.pIntPrtUserL);
                        m_ticket_info.pIntPrtUserL = IntPtr.Zero;
                        if (m_ticket_info.pIntSAPClientL == IntPtr.Zero)
                                Marshal.FreeHGlobal(m_ticket_info.pIntSAPClientL);
                        m_ticket_info.pIntSAPClientL = IntPtr.Zero;
                        if (m_ticket_info.pIntSAPLanguL == IntPtr.Zero)
                                Marshal.FreeHGlobal(m_ticket_info.pIntSAPLanguL);
                        m_ticket_info.pIntSAPLanguL = IntPtr.Zero;
                        if (m_ticket_info.pIntSAPUserL == IntPtr.Zero)
                                Marshal.FreeHGlobal(m_ticket_info.pIntSAPUserL);
                        m_ticket_info.pIntSAPUserL = IntPtr.Zero;
                        if (m_ticket_info.pIntSysIDL == IntPtr.Zero)
                                Marshal.FreeHGlobal(m_ticket_info.pIntSysIDL);
                        m_ticket_info.pIntSysIDL = IntPtr.Zero;
                        if (m_ticket_info.ptmValidity == IntPtr.Zero)
                                Marshal.FreeHGlobal(m_ticket_info.ptmValidity);
                        m_ticket_info.ptmValidity = IntPtr.Zero;
                        if (m_ticket_info.ptmCreTime == IntPtr.Zero)
                                Marshal.FreeHGlobal(m_ticket_info.ptmCreTime);
                        m_ticket_info.ptmCreTime = IntPtr.Zero;
                }

                // load and start external SSF library
                public int StartSSOExt(String ssfLibrary)
                {
                        return NativeMethods.MySapInitialize(ssfLibrary);
                }

                // stop and unload external SSF library
                public int StopSSOExt()
                {
                        return NativeMethods.MySapShutdown();
                }

                // initializse structure
                private void initLongInfo()
                {
                        if (IntPtr.Size == 8)
                        {
                                m_ticket_info.struct_size = 128; /* sizeof struct on 64 bit platform */
                        } 
                        else if (IntPtr.Size == 4)
                        {
                                m_ticket_info.struct_size = 64;  /* sizeof struct on 32 bit platform */
                        }
                        m_ticket_info.sSAPCodepage = new byte[4];
                        m_ticket_info.sSAPCodepage[0] = (byte)'4';
                        m_ticket_info.sSAPCodepage[1] = (byte)'1';
                        m_ticket_info.sSAPCodepage[2] = (byte)'1';
                        m_ticket_info.sSAPCodepage[3] = (byte)'0';
                        if (m_ticket_info.pIntPrtUserL == IntPtr.Zero)
                                m_ticket_info.pIntPrtUserL = Marshal.AllocHGlobal(new IntPtr(256));
                        else
                                Marshal.WriteIntPtr(m_ticket_info.pIntPrtUserL, new IntPtr(256));
                        m_ticket_info.pPrtUser = new string(' ', 256);
                        if (m_ticket_info.pIntAuthSchemeL == IntPtr.Zero)
                                m_ticket_info.pIntAuthSchemeL = Marshal.AllocHGlobal(new IntPtr(256));
                        else
                                Marshal.WriteIntPtr(m_ticket_info.pIntAuthSchemeL, new IntPtr(256));
                        m_ticket_info.pAuthScheme = new string(' ', 256);
                        if (m_ticket_info.pIntSAPClientL == IntPtr.Zero)
                                m_ticket_info.pIntSAPClientL = Marshal.AllocHGlobal(new IntPtr(3));
                        else
                                Marshal.WriteIntPtr(m_ticket_info.pIntSAPClientL, new IntPtr(3));
                        m_ticket_info.pSAPClient = new string(' ', 3);
                        if (m_ticket_info.pIntSAPLanguL == IntPtr.Zero)
                                m_ticket_info.pIntSAPLanguL = Marshal.AllocHGlobal(new IntPtr(2));
                        else
                                Marshal.WriteIntPtr(m_ticket_info.pIntSAPLanguL, new IntPtr(2));
                        m_ticket_info.pSAPLangu = new string(' ', 2);
                        if (m_ticket_info.pIntSAPUserL == IntPtr.Zero)
                                m_ticket_info.pIntSAPUserL = Marshal.AllocHGlobal(new IntPtr(12));
                        else
                                Marshal.WriteIntPtr(m_ticket_info.pIntSAPUserL, new IntPtr(12));
                        m_ticket_info.pSAPUser = new string(' ', 12);
                        if (m_ticket_info.pIntSysIDL == IntPtr.Zero)
                                m_ticket_info.pIntSysIDL = Marshal.AllocHGlobal(new IntPtr(8));
                        else
                                Marshal.WriteIntPtr(m_ticket_info.pIntSysIDL, new IntPtr(8));
                        m_ticket_info.pSysID = new string(' ', 8);
                }

                // reset structure
                private void resetLongInfo()
                {
                        if (m_ticket_info.pIntAuthSchemeL != IntPtr.Zero)
                                Marshal.WriteIntPtr(m_ticket_info.pIntAuthSchemeL, new IntPtr(256));
                        if (m_ticket_info.pIntPrtUserL != IntPtr.Zero)
                                Marshal.WriteIntPtr(m_ticket_info.pIntPrtUserL, new IntPtr(256));
                        if (m_ticket_info.pIntSAPClientL != IntPtr.Zero)
                                Marshal.WriteIntPtr(m_ticket_info.pIntSAPClientL, new IntPtr(3));
                        if (m_ticket_info.pIntSAPLanguL != IntPtr.Zero)
                                Marshal.WriteIntPtr(m_ticket_info.pIntSAPLanguL, new IntPtr(2));
                        if (m_ticket_info.pIntSAPUserL != IntPtr.Zero)
                                Marshal.WriteIntPtr(m_ticket_info.pIntSAPUserL, new IntPtr(12));
                        if (m_ticket_info.pIntSysIDL != IntPtr.Zero)
                                Marshal.WriteIntPtr(m_ticket_info.pIntSysIDL, new IntPtr(8));
                        if (m_ticket_info.ptmCreTime != IntPtr.Zero)
                                Marshal.WriteIntPtr(m_ticket_info.ptmCreTime, new IntPtr(0));
                        if (m_ticket_info.ptmValidity != IntPtr.Zero)
                                Marshal.WriteIntPtr(m_ticket_info.ptmValidity, new IntPtr(0));
                }

                // evaluate a SAP logon ticket from Portal
                public int EvalPortalTicket(String ticket,
                                         String addBook,
                                         String passWord,
                                     ref TICKET_PORTAL_INFO ticket_info)
                {
                        int sso_rc = 0;
                        byte[] cert = new byte[8400];
                        UInt32[] certlen = new UInt32[1];
                        certlen[0] = 8400;
                        if (m_ticket_info.pIntPrtUserL == IntPtr.Zero)
                        {
                                m_ticket_info.pIntPrtUserL = Marshal.AllocHGlobal(new IntPtr(256));
                                m_ticket_info.pPrtUser = new string(' ', 256);
                        }
                        resetLongInfo();
                        sso_rc = NativeMethods.MySapEvalLogonTicketEx(ticket,
                                                           addBook,
                                                           passWord,
                                                           cert,
                                                           certlen,
                                                           ref m_ticket_info);

                        if (sso_rc != 0)
                        {
                                ticket_info.sapClient = "";
                                ticket_info.sapPortalUser = "";
                                ticket_info.sapSysID = "";
                                ticket_info.sapUser = "";
                                return sso_rc;
                        }
                        else
                        {
                                ticket_info.sapUser = m_ticket_info.pSAPUser;
                                ticket_info.sapSysID = m_ticket_info.pSysID;
                                ticket_info.sapClient = m_ticket_info.pSAPClient;
                                ticket_info.sapPortalUser = m_ticket_info.pPrtUser;
                                ticket_info.sapCert = new byte[certlen[0]];
                                Array.Copy(cert, ticket_info.sapCert, certlen[0]);
                                return 0;
                        }
                }

                // evaluate a SAP logon ticket from Portal with all possibly values
                public int EvalPortalTicket(String ticket,
                                         String addBook,
                                         String passWord,
                                     ref TICKET_PORTAL_INFO_LONG ticket_info)
                {
                        int sso_rc = 0;
                        byte[] cert = new byte[8400];
                        UInt32[] certlen = new UInt32[1];
                        certlen[0] = 8400;
                        if (m_ticket_info.pIntPrtUserL == IntPtr.Zero)
                        {
                                m_ticket_info.pIntPrtUserL = Marshal.AllocHGlobal(new IntPtr(256));
                                m_ticket_info.pPrtUser = new string(' ',256);
                        }
                        if (m_ticket_info.pIntAuthSchemeL == IntPtr.Zero)
                        {
                                m_ticket_info.pIntAuthSchemeL = Marshal.AllocHGlobal(new IntPtr(256));
                                m_ticket_info.pAuthScheme = new string(' ',256);
                        }
                        if (m_ticket_info.ptmValidity == IntPtr.Zero)
                        {
                                m_ticket_info.ptmValidity = Marshal.AllocHGlobal(new IntPtr(0));
                        }
                        resetLongInfo();
                        sso_rc = NativeMethods.MySapEvalLogonTicketEx(ticket,
                                                           getFullFilePath(addBook),
                                                           passWord, 
                                                           cert,
                                                           certlen,
                                                           ref m_ticket_info);
                        if (sso_rc != 0)
                        {
                                ticket_info.sapAuthSchema = "";
                                ticket_info.sapClient = "";
                                ticket_info.sapPortalUser = "";
                                ticket_info.sapSysID = "";
                                ticket_info.sapUser = "";
                                ticket_info.validity = 0;
                                return sso_rc;
                        }
                        else
                        {
                                ticket_info.sapUser = m_ticket_info.pSAPUser;
                                ticket_info.sapSysID = m_ticket_info.pSysID;
                                ticket_info.sapClient = m_ticket_info.pSAPClient;
                                ticket_info.sapAuthSchema = m_ticket_info.pAuthScheme;
                                ticket_info.sapPortalUser = m_ticket_info.pPrtUser;
                                if (IntPtr.Size == 8)
                                {   /* time_t is int64 */
                                    ticket_info.validity = (Int64)Marshal.ReadInt64(m_ticket_info.ptmValidity);
                                }
                                else if (IntPtr.Size == 4)
                                {   /* time_t is int32 */
                                    ticket_info.validity = (Int64)Marshal.ReadInt32(m_ticket_info.ptmValidity);
                                }   
                                ticket_info.sapCert = new byte[certlen[0]];
                                Array.Copy(cert, ticket_info.sapCert, certlen[0]);
                                return 0;
                        }
                }

                // evalute a SAP logon ticket
                public static int EvalSAPTicket(String ticket,
                                         String addBook,
                                         String passWord,
                                     ref TICKET_INFO ticket_info)
                {
                        int sso_rc = 0;
                        byte [] user = new byte[256];
                        byte[]  sysid = new byte[256];
                        byte[]  clients = new byte[256];
                        byte[] cert = new byte[8400];
                        UInt32[] certlen = new UInt32[1];
                        UInt32 [] userlen = new UInt32[1];
                        UInt32[] syslen = new UInt32[1];
                        UInt32[] clientlen = new UInt32[1];
                        userlen[0] = 256;
                        syslen[0] = 256;
                        clientlen[0] = 256;
                        certlen[0] = 8400;

                        sso_rc = NativeMethods.MySapEvalLogonTicket(ticket,
                                                                    getFullFilePath(addBook),
                                                                    passWord,
                                                                    user,
                                                                    userlen,
                                                                    cert,
                                                                    certlen,
                                                                    sysid,
                                                                    syslen,
                                                                    clients,
                                                                    clientlen,
                                                                    IntPtr.Zero);
                        if(sso_rc != 0)
                        {
                                ticket_info.sapClient = "";
                                ticket_info.sapUser = "";
                                ticket_info.sapSysID = "";
                                return sso_rc;
                        }
                        else
                        {
                                ticket_info.sapUser = 
                                        System.Text.Encoding.UTF8.GetString(user, 0, (int)userlen[0]);
                                ticket_info.sapSysID =
                                        System.Text.Encoding.UTF8.GetString(sysid, 0, (int)syslen[0]);
                                ticket_info.sapClient =
                                        System.Text.Encoding.UTF8.GetString(clients, 0, (int)clientlen[0]);
                                ticket_info.sapCert = new byte[certlen[0]];
                                Array.Copy(cert, ticket_info.sapCert, certlen[0]);
                                return 0;
                        }
                }

                // create a SAP assertion ticket
                public String CreateAssertionTicket(
                                               String addBook,
                                               String passWord,
                                               String myOwnSystemId,
                                               String myOwnSystemClient,
                                               String extSystemId,
                                               String extSystemClient,
                                               String sapUser,
                                               String portalUser,
                                               String language,
                                               String authSchema)
                {
                        byte []ticket = new byte[4096];
                        Int32[] ticketlen = new Int32[1];
                        String sTicket = null;
                        int sso_rc = 0;
                        ticketlen[0] = 4096;
                        initLongInfo();
                        if (extSystemId == null)
                        {
                                m_ticket_info.pSysID = null;
                        }
                        else
                        {
                                m_ticket_info.pSysID = extSystemId;
                                Marshal.WriteIntPtr(m_ticket_info.pIntSysIDL, new IntPtr(extSystemId.Length));
                        }
                        if (extSystemClient == null)
                        {
                                m_ticket_info.pSAPClient = null;
                        }
                        else
                        {
                                m_ticket_info.pSAPClient = extSystemClient;
                                Marshal.WriteIntPtr(m_ticket_info.pIntSAPClientL,new IntPtr(extSystemClient.Length));
                        }
                        if (sapUser == null)
                        {
                                m_ticket_info.pSAPUser = null;
                        }
                        else
                        {
                                m_ticket_info.pSAPUser = sapUser;
                                Marshal.WriteIntPtr(m_ticket_info.pIntSAPUserL,new IntPtr(sapUser.Length));
                        }
                        if (portalUser == null)
                        {
                                m_ticket_info.pPrtUser = null;
                        }
                        else
                        {
                                m_ticket_info.pPrtUser = portalUser;
                                Marshal.WriteIntPtr(m_ticket_info.pIntPrtUserL,new IntPtr(portalUser.Length));
                        }
                        if (authSchema == null)
                        {
                                m_ticket_info.pAuthScheme = null;
                        }
                        else
                        {
                                m_ticket_info.pAuthScheme = authSchema;
                                Marshal.WriteIntPtr(m_ticket_info.pIntAuthSchemeL,new IntPtr(authSchema.Length));
                        }
                        if (language == null)
                        {
                                m_ticket_info.pSAPLangu = null;
                        }
                        else
                        {
                                m_ticket_info.pSAPLangu = language;
                                Marshal.WriteIntPtr(m_ticket_info.pIntSAPLanguL,new IntPtr(language.Length));
                        }
                        sso_rc = NativeMethods.MySapCreateAssertionTicket(myOwnSystemId,
                                                                          myOwnSystemClient,
                                                                          getFullFilePath(addBook),
                                                                          passWord,
                                                                          ref m_ticket_info,
                                                                          ticket,
                                                                          ticketlen);
                        if (sso_rc != 0)
                        {
                                sTicket = "";
                        }
                        else
                        {
                                sTicket = System.Text.Encoding.UTF8.GetString(ticket, 0, (int)ticketlen[0]);
                        }
                        return sTicket;
                }

                // evaluate a SAP assertion ticket
                public int EvalAssertionTicket(String ticket,
                                               String myOwnSystemId,
                                               String myOwnSystemClient,
                                               String addBook,
                                               String passWord,                                               
                                     ref TICKET_PORTAL_INFO_LONG ticket_info)
                {
                        int sso_rc = 0;
                        if (m_ticket_info.pIntPrtUserL == IntPtr.Zero)
                        {
                                m_ticket_info.pIntPrtUserL = Marshal.AllocHGlobal(new IntPtr(256));
                                m_ticket_info.pPrtUser = new string(' ', 256);
                        }
                        if (m_ticket_info.pIntAuthSchemeL == IntPtr.Zero)
                        {
                                m_ticket_info.pIntAuthSchemeL = Marshal.AllocHGlobal(new IntPtr(256));
                                m_ticket_info.pAuthScheme = new string(' ', 256);
                        }
                        if (m_ticket_info.ptmValidity == IntPtr.Zero)
                        {
                                m_ticket_info.ptmValidity = Marshal.AllocHGlobal(new IntPtr(0));
                        }
                        resetLongInfo();
                        sso_rc = NativeMethods.MySapEvalAssertionTicket(ticket,
                                                                        getFullFilePath(addBook),
                                                                        passWord,
                                                                        myOwnSystemId,
                                                                        myOwnSystemClient,
                                                                        ref m_ticket_info);

                        ticket_info.sapCert = null;
                        ticket_info.validity = 0;
                        if (sso_rc != 0)
                        {
                                ticket_info.sapClient = "";
                                ticket_info.sapPortalUser = "";
                                ticket_info.sapSysID = "";
                                ticket_info.sapUser = "";
                                ticket_info.sapAuthSchema = "";
                                return sso_rc;
                        }
                        else
                        {
                                ticket_info.sapUser = m_ticket_info.pSAPUser;
                                ticket_info.sapSysID = m_ticket_info.pSysID;
                                ticket_info.sapClient = m_ticket_info.pSAPClient;
                                ticket_info.sapPortalUser = m_ticket_info.pPrtUser;
                                ticket_info.sapAuthSchema = m_ticket_info.pAuthScheme;
                                return 0;
                        }
                }

                // set property in SAPSSOEXT
                public int SetProperty(String pName, String pValue)
                {                        
                        return NativeMethods.SsoExtSetProperty(pName, pValue);
                }

                // get property from SAPSSOEXT
                public String GetProperty(String pName)
                {
                        return NativeMethods.SsoExtGetProperty(pName);
                }

                // load a PSE file into the memory of SAPSSOEXT
                public int LoadPSE(byte[] pseArray, String psePassword)
                {
                        return NativeMethods.MySapLoadTicketKey(pseArray, pseArray.Length, psePassword, 0, 0);
                }

                // load a X509 certificate (for ticket verification) into the memory of SAPSSOEXT
                public int LoadX509Certificate(byte[] xCertificate)
                {
                        return NativeMethods.MySapLoadTicketKey(xCertificate, xCertificate.Length, null, 0, 1);
                }

                // parse a X.509 certificate
                // see sapssoext.h for possibly values of "info"
                public static String ParseCertificate(byte[] certArray,
                                                    int certLength,
                                                    String info)
                {
                        int sso_rc = 0;
                        byte [] buffer = new byte[8600];
                        Int32 [] bufferlen = new Int32[1];
                        bufferlen[0] = 8600;

                        sso_rc = NativeMethods.MySapParseCertificate(certArray,
                                                                     (uint)certLength,
                                                                     info, /* see sapssoext.h */
                                                                     buffer,
                                                                     bufferlen);
                        if (sso_rc != 0)
                                return "";
                        else
                                return System.Text.Encoding.UTF8.GetString(buffer, 0, (int)bufferlen[0]);
                }               

                // parse a X.509 certificate
                // see sapssoext.h for possibly values of "info"
                public static String ParseCertificate(byte[] certArray,
                                                      String info)
                {
                        return ParseCertificate(certArray, certArray.Length, info);
                }
                                                    
                // get the full path to a file
                private static String getFullFilePath(string filename)
                {
                        String path;

                        if (filename == null)
                                return null;
                        //if (Path.HasExtension(filename))
                        //{
                                path = filename;
                        //}
                        //else
                        //{
                        //        path = Path.GetFullPath(filename + ".pse");
                        //}
                        if (!File.Exists(path))
                                throw new FileNotFoundException("File " + filename + " does not exists", filename);
                        return path;
                }
                // read the ticket string from a File
                private static String getTicket(string filename)
                {
                        if (filename == null)
                                return null;
                        try
                        {
                                // Create an instance of StreamReader to read from a file.
                                // The using statement also closes the StreamReader.
                                using (StreamReader sr = new StreamReader(filename))
                                {
                                        String line = sr.ReadToEnd();
                                        sr.Close();
                                        return line;
                                }
                        }
                        catch (Exception e)
                        {
                                // Let the user know what went wrong.
                                Console.WriteLine("The file could not be read:");
                                Console.WriteLine(e.Message);
                                throw new FieldAccessException("File " + filename + " could not be read", e);
                        }

                }
                // parse the arguments for an option
                private static String getCommandParam(string[] args, string option)
                {
                        for (int i = 0; i < args.Length; i++)
                        {
                                if (args[i].Equals(option) && args.Length > i + 1)
                                {
                                        return args[i + 1];
                                }
                        }
                        return null;
                }
                // print help to console
                private static void PrintHelp()
                {
                        Console.WriteLine("   ssoext -i <ticket_file> [-c (create) <ticket>] [-L <SSF_LIB>]");
                        Console.WriteLine("   [-p <file containing public key>] [-pwd <PSE password>]");
                        Console.WriteLine("   [-crt <file containing public certificate>]");
                        Console.WriteLine("   [-t <developer trace file>] [-l <level of trace (1, 2 or 3)>]");
                }

                // main method, used for testing purposes
                static void Main(string[] args)
                {
                        int sso_rc = 1;                            
                        TICKET_PORTAL_INFO_LONG ticketinfo = new TICKET_PORTAL_INFO_LONG();
                        String ticket = getTicket(getCommandParam(args, "-i"));
                        String pseFile = getCommandParam(args, "-p");
                        String certFile = getCommandParam(args, "-crt");
                        SAPSSOEXT ssoext = new SAPSSOEXT(getCommandParam(args,"-L"));

                        if (ticket == null && pseFile == null && certFile == null)
                        {
                                PrintHelp();
                                return;
                        }
                        if (getCommandParam(args, "-t") != null)
                        {
                                ssoext.SetProperty("SAP_EXT_TRC", getCommandParam(args, "-t"));
                                ssoext.SetProperty("SAP_EXT_TRL", getCommandParam(args, "-l"));
                        }
                        if (pseFile != null || certFile != null)
                        {
                                byte[] buffer = new byte[8192];
                                int l;
                                using (BinaryReader br = new BinaryReader(new FileStream(pseFile == null ? certFile : pseFile, FileMode.Open)))
                                {
                                        l = br.Read(buffer,0,8192);                                        
                                        br.Close();
                                }
                                byte[] bybuffer = new byte[l];
                                for (int i = 0; i < l; i++) bybuffer[i] = buffer[i];
                                if (pseFile != null)
                                        ssoext.LoadPSE(bybuffer, getCommandParam(args, "-pwd"));
                                else
                                        ssoext.LoadX509Certificate(bybuffer);
                                
                        }
                        if (getCommandParam(args, "-c") != null)
                        {
                                String _ticket = ssoext.CreateAssertionTicket(null, null,
                                                             "EXT", "999", ssoext.GetProperty("SAP_EXT_SYSID"),
                                                             ssoext.GetProperty("SAP_EXT_CLIENT"), "SAPUSER", "PORTALUSER", "EN",
                                                             "basicauthentication");
                                using (StreamWriter sw = new StreamWriter(getCommandParam(args, "-c")))
                                {
                                        sw.Write(_ticket);
                                        sw.Close();
                                }
                                ticket = _ticket;
                        }
                        if (0 == (sso_rc = ssoext.EvalPortalTicket(ticket,
                                                                   null,
                                                                   null,
                                                                   ref ticketinfo)))
                        {                                
                                Console.WriteLine("***********************************************");
                                Console.WriteLine(" Output of program:");
                                Console.WriteLine("***********************************************");
                                Console.Write("\n");
                                Console.Write("The ticket\n\n" + ticket + "\n\n");
                                Console.WriteLine("was successfully validated.\n");
                                Console.WriteLine("User     : " + ticketinfo.sapUser);
                                Console.WriteLine("Ident of ticket issuing system:");
                                Console.WriteLine("Sysid    : " + ticketinfo.sapSysID);
                                Console.WriteLine("Client   : " + ticketinfo.sapClient);
                                Console.WriteLine("External ident of user:");
                                Console.WriteLine("PortalUsr: " + ticketinfo.sapPortalUser);
                                Console.WriteLine("Auth     : " + ticketinfo.sapAuthSchema);
                                Console.WriteLine("Ticket validity in seconds:");
                                Console.WriteLine("Valid (s): " + ticketinfo.validity);
                                Console.WriteLine("Certificate data of issuing system:");
                                Console.WriteLine("Subject  : " + ParseCertificate(ticketinfo.sapCert, "SUBJECT"));
                                Console.WriteLine("Issuer   : " + ParseCertificate(ticketinfo.sapCert, "ISSUER"));
                                Console.Write("\n");
                        }
                        else
                        {
                                Console.WriteLine("EvalPortalTicket returned with error code: " + sso_rc );
                        }
                }
        }
}