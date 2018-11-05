package org.modelio.microservicesnetcore.api;

import org.modelio.microservicesnetcore.impl.MicroserviceDotnetCoreModule;

public class ModuleConstants {
	
	private static MicroserviceDotnetCoreModule myModule = MicroserviceDotnetCoreModule.getInstance();
	
	public static String MODULE_NAME=MicroserviceDotnetCoreModule.getInstance().getName();
	
	public static String PIM_Suffix = "pim";
	public static String PSM_Suffix = "psm";
	
	public static String getPSMName(String PIMname)
	{
		return PIMname.replaceFirst(PIM_Suffix, PSM_Suffix);
	}
}
