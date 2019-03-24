package org.modelio.microservicesnetcore.command;

import java.util.List;

import org.eclipse.jface.dialogs.MessageDialog;
import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.ITransaction;
import org.modelio.api.module.IModule;
import org.modelio.api.module.command.DefaultModuleCommandHandler;
import org.modelio.api.module.context.log.ILogService;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PimStereotypeValidator;
import org.modelio.microservicesnetcore.helper.PsmBuilder;
import org.modelio.microservicesnetcore.helper.PsmStereotypeValidator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmIRepositoryOrchestrator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmIServiceOrchestrator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmModelOrchestrator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmRepositoryOrchestrator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmServiceOrchestrator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GenerateIRepoProjectCodeOrchestrator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmControllerOrchestrator;
import org.modelio.vcore.smkernel.mapi.MObject;

public class GenerateRepoProjetCodeCommand extends DefaultModuleCommandHandler {
    /**
     * Constructor.
     */
    public GenerateRepoProjetCodeCommand() {
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
    		if(!PsmStereotypeValidator.IsPsmRepositoryPackage((Package)elt))
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
        logService.info("Generate PSM Packages - actionPerformed(...)");
        IModelingSession _session = module.getModuleContext().getModelingSession();
        try{
			
	        for(MObject e :selectedElements )
	        {
	        	GenerateIRepoProjectCodeOrchestrator iRepoOrchest = new GenerateIRepoProjectCodeOrchestrator(module);
	        	iRepoOrchest.Execute(e);
		        
		        GenerateRepoProjectCodeOrchestrator repoOrchest = new GenerateRepoProjectCodeOrchestrator(module);
		        repoOrchest.Execute(e);
	        }
		}
		catch (Exception e) {
			throw e;
		}
        MessageDialog.openInformation(null, "Information", ".net IRepository Project has been generated !");
    }


}
