package org.modelio.microservicesnetcore.api;

import org.modelio.microservicesnetcore.impl.MicroserviceDotnetCoreModule;

public class ModuleConstants {
	
	private static MicroserviceDotnetCoreModule myModule = MicroserviceDotnetCoreModule.getInstance();
	
	public static String MODULE_NAME=MicroserviceDotnetCoreModule.getInstance().getName();
	
	public static String PIM_Suffix = "pim";
	public static String PSM_Suffix = "psm";

	
	public static String PSM_IRepositoryName = "I%repository%Repository";
	public static String PSM_RepositoryName = "%repository%Repository";
	public static String PSM_IServiceName = "I%service%Service";
	public static String PSM_ServiceName = "%service%Service";
	public static String PSM_ControllerName = "%controller%Controller";
	public static String PSM_ModelPackageName = "Model";
	
	public static String getPSMName(String PIMname)
	{
		return PIMname.replaceFirst(PIM_Suffix, PSM_Suffix);
	}
	
	public static String getIRepositoryName(String conceptName)
	{
		return PSM_IRepositoryName.replaceFirst("%repository%", conceptName);
	}
	public static String getRepositoryName(String conceptName)
	{
		return PSM_RepositoryName.replaceFirst("%repository%", conceptName);
	}
	public static String getIServiceName(String conceptName)
	{
		return PSM_IServiceName.replaceFirst("%service%", conceptName);
	}
	public static String getServiceName(String conceptName)
	{
		return PSM_ServiceName.replaceFirst("%service%", conceptName);
	}
	public static String getControllerName(String conceptName)
	{
		return PSM_ControllerName.replaceFirst("%controller%", conceptName);
	}
}
