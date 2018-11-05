package org.modelio.microservicesnetcore.api;

import org.modelio.api.modelio.meta.IMetamodelService;
import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.context.IModuleContext;
import org.modelio.metamodel.uml.infrastructure.NoteType;
import org.modelio.microservicesnetcore.impl.MicroserviceDotnetCoreModule;
import org.modelio.vcore.smkernel.mapi.MClass;
import org.modelio.vcore.smkernel.mapi.MMetamodel;
import org.modelio.vcore.smkernel.mapi.MObject;

public class ModuleNoteType {
	
	private static MicroserviceDotnetCoreModule myModule = MicroserviceDotnetCoreModule.getInstance();
	
	
	
	public static NoteType GetNoteType(IModelingSession session,Class<? extends MObject> type, String name) {
		NoteType result = null;
		IModuleContext ctx = myModule.getModuleContext();
		IMetamodelService mmService = ctx.getModelioServices().getMetamodelService();
		MMetamodel metamodel = mmService.getMetamodel();
		MClass mClass = metamodel.getMClass(type);
		result = session.getMetamodelExtensions().getNoteType(ModuleConstants.MODULE_NAME,name,mClass);
		return result;
	}
	
	
}
