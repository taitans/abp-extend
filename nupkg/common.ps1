# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of solutions
$solutions = (
    "modules/identity",
    "modules/identityserver",
    "modules/ocelot-management",
    "modules/audit-logging",
    "modules/saas-management"
)

# List of projects
$projects = (
  
    # modules/identity
    "modules/identity/src/Taitans.Abp.Identity.Application.Contracts",
    "modules/identity/src/Taitans.Abp.Identity.Application",
    "modules/identity/src/Taitans.Abp.Identity.Domain",
    "modules/identity/src/Taitans.Abp.Identity.Domain.Shared",
    "modules/identity/src/Taitans.Abp.Identity.EntityFrameworkCore",
    "modules/identity/src/Taitans.Abp.Identity.HttpApi.Client",
    "modules/identity/src/Taitans.Abp.Identity.HttpApi",
    "modules/identity/src/Taitans.Abp.Identity.MongoDB",
    
    # modules/identityserver
    "modules/identityserver/src/Taitans.Abp.IdentityServer.Application.Contracts",
    "modules/identityserver/src/Taitans.Abp.IdentityServer.Application",
    "modules/identityserver/src/Taitans.Abp.IdentityServer.HttpApi",
    "modules/identityserver/src/Taitans.Abp.IdentityServer.HttpApi.Client",
    "modules/identityserver/src/Taitans.Abp.IdentityServer.Domain",
    "modules/identityserver/src/Taitans.Abp.IdentityServer.Domain.Shared",
    "modules/identityserver/src/Taitans.Abp.IdentityServer.EntityFrameworkCore",
    "modules/identityserver/src/Taitans.Abp.IdentityServer.MongoDB",

    # modules/ocelot-management
    "modules/ocelot-management/src/Taitans.Abp.OcelotManagement.Application.Contracts",
    "modules/ocelot-management/src/Taitans.Abp.OcelotManagement.Application",
    "modules/ocelot-management/src/Taitans.Abp.OcelotManagement.HttpApi",
    "modules/ocelot-management/src/Taitans.Abp.OcelotManagement.HttpApi.Client",
    "modules/ocelot-management/src/Taitans.Abp.OcelotManagement.Domain",
    "modules/ocelot-management/src/Taitans.Abp.OcelotManagement.Domain.Shared",
    "modules/ocelot-management/src/Taitans.Abp.OcelotManagement.EntityFrameworkCore",
    "modules/ocelot-management/src/Taitans.Abp.OcelotManagement.MongoDB",
    "modules/ocelot-management/src/Taitans.Ocelot.Provider.Abp",

    # modules/audit-logging"
    "modules/audit-logging/src/Taitans.Abp.AuditLogging.Application.Contracts",
    "modules/audit-logging/src/Taitans.Abp.AuditLogging.Application",
    "modules/audit-logging/src/Taitans.Abp.AuditLogging.HttpApi",
    "modules/audit-logging/src/Taitans.Abp.AuditLogging.HttpApi.Client",
    "modules/audit-logging/src/Taitans.Abp.AuditLogging.Domain",
    "modules/audit-logging/src/Taitans.Abp.AuditLogging.Domain.Shared",
    "modules/audit-logging/src/Taitans.Abp.AuditLogging.EntityFrameworkCore",
    "modules/audit-logging/src/Taitans.Abp.AuditLogging.MongoDB",

    # modules/saas-management
    "modules/saas-management/src/Taitans.Abp.SaasManagement.Application.Contracts",
    "modules/saas-management/src/Taitans.Abp.SaasManagement.Application",
    "modules/saas-management/src/Taitans.Abp.SaasManagement.HttpApi",
    "modules/saas-management/src/Taitans.Abp.SaasManagement.HttpApi.Client",
    "modules/saas-management/src/Taitans.Abp.SaasManagement.Domain",
    "modules/saas-management/src/Taitans.Abp.SaasManagement.Domain.Shared",
    "modules/saas-management/src/Taitans.Abp.SaasManagement.EntityFrameworkCore",
    "modules/saas-management/src/Taitans.Abp.SaasManagement.MongoDB"
)