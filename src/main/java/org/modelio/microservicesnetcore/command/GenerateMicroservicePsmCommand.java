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
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmModelOrchestrator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmRepositoryOrchestrator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmServiceOrchestrator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmControllerOrchestrator;
import org.modelio.vcore.smkernel.mapi.MObject;

public class GenerateMicroservicePsmCommand extends DefaultModuleCommandHandler {
    /**
     * Constructor.
     */
    public GenerateMicroservicePsmCommand() {
        super();
    }

    /**
     * @see org.modelio.api.module.commands.DefaultModuleContextualCommand#accept(java.util.List,
     *      org.modelio.api.module.IModule)
     */
    @Override
    public boolean accept(List<MObject> selectedElements, IModule module) {
        // Check that there is only one selected element
    	if(selectedElements.size()==1)
    	{
    		MObject elt=selectedElements.get(0);
    		if((elt instanceof Package) && PimStereotypeValidator.isMicroservice((Package)elt))
    		{
    			return true;
    		}
    	}
        return false;
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
        try (ITransaction t = _session.createTransaction("Create PSM Microservice")) {
			
	        for(MObject e :selectedElements )
	        {
	        		
		        GeneratePsmModelOrchestrator orchestModel = new GeneratePsmModelOrchestrator(module);
		        orchestModel.Execute(e);
		        
		        GeneratePsmRepositoryOrchestrator orchestRepo = new GeneratePsmRepositoryOrchestrator(module);
		        orchestRepo.Execute(e);
		        
		        GeneratePsmServiceOrchestrator orchestServ = new GeneratePsmServiceOrchestrator(module);
		        orchestServ.Execute(e);
		        
		        GeneratePsmControllerOrchestrator orchestApi = new GeneratePsmControllerOrchestrator(module);
		        orchestApi.Execute(e);
	        }
	        t.commit();
		}
		catch (Exception e) {
			throw e;
		}
        MessageDialog.openInformation(null, "Information", "PSM has been generated !");
    }


}
