package org.modelio.microservicesnetcore.command;

import java.util.List;

import org.eclipse.jface.dialogs.MessageDialog;
import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.api.module.command.DefaultModuleCommandHandler;
import org.modelio.api.module.context.log.ILogService;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.psm.generator.GeneratePsmVersionOrchestrator;
import org.modelio.microservicesnetcore.psm.helper.PimStereotypeValidator;
import org.modelio.vcore.smkernel.mapi.MObject;

public class GenerateMicroservicePsmVersionCommand extends DefaultModuleCommandHandler {
    /**
     * Constructor.
     */
    public GenerateMicroservicePsmVersionCommand() {
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
    		if(!PimStereotypeValidator.isMicroserviceModel((Package)elt))
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

        for(MObject e :selectedElements )
        {
	        GeneratePsmVersionOrchestrator orchestrator = new GeneratePsmVersionOrchestrator(module);
	        orchestrator.Execute(e);
        }
        
        MessageDialog.openInformation(null, "Information", "PSM has been generated !");
    }


}
