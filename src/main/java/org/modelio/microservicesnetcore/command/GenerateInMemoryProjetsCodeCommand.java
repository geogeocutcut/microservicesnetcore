package org.modelio.microservicesnetcore.command;

import java.util.List;

import org.eclipse.jface.dialogs.MessageDialog;
import org.modelio.api.module.IModule;
import org.modelio.api.module.command.DefaultModuleCommandHandler;
import org.modelio.api.module.context.log.ILogService;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PsmStereotypeValidator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GenerateInMemoryProjectsCodeOrchestrator;
import org.modelio.vcore.smkernel.mapi.MObject;

public class GenerateInMemoryProjetsCodeCommand extends DefaultModuleCommandHandler {
    /**
     * Constructor.
     */
    public GenerateInMemoryProjetsCodeCommand() {
        super();
    }

    /**
     * @see org.modelio.api.module.commands.DefaultModuleContextualCommand#accept(java.util.List,
     *      org.modelio.api.module.IModule)
     */
    @Override
    public boolean accept(List<MObject> selectedElements, IModule module) {
        // Check that there is only one selected element
    	for(MObject elt :  selectedElements)
    	{
    		if(!(elt instanceof Package))
    		{
    			return false;
    		}
    		if(!PsmStereotypeValidator.IsPsmMicroservicePackage((Package)elt))
    		{
    			return false;
    		}
    	}
        return true;
    }

    /**
     * @see org.modelio.api.module.commands.DefaultModuleContextualCommand#actionPerformed(java.util.List,
     *      org.modelio.api.module.IModule)
     */
    @Override
    public void actionPerformed(List<MObject> selectedElements, IModule module) {
        ILogService logService = module.getModuleContext().getLogService();
        logService.info("Generate InMemory Code - actionPerformed(...)");
        try{
			
	        for(MObject e :selectedElements )
	        {
	        	GenerateInMemoryProjectsCodeOrchestrator orchestModel = new GenerateInMemoryProjectsCodeOrchestrator(module);
		        orchestModel.Execute(e);
	        }
		}
		catch (Exception e) {
			throw e;
		}
        MessageDialog.openInformation(null, "Information", ".net InMemory Microservice have been generated !");
    }


}
