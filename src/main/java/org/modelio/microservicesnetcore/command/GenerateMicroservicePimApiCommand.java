package org.modelio.microservicesnetcore.command;

import java.util.List;

import org.eclipse.jface.dialogs.MessageDialog;
import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.api.module.command.DefaultModuleCommandHandler;
import org.modelio.api.module.context.log.ILogService;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PimStereotypeValidator;
import org.modelio.microservicesnetcore.psm.generator.orchestrator.GeneratePsmModelOrchestrator;
import org.modelio.vcore.smkernel.mapi.MObject;

public class GenerateMicroservicePimApiCommand extends DefaultModuleCommandHandler {
    /**
     * Constructor.
     */
    public GenerateMicroservicePimApiCommand() {
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
    		if(!PimStereotypeValidator.isMicroservice((Package)elt))
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
        logService.info("Generate PIM Api - actionPerformed(...)");

        for(MObject e :selectedElements )
        {
	        GeneratePimApiOrchestrator orchestrator = new GeneratePimApiOrchestrator(module);
	        orchestrator.Execute(e);
        }
        
        MessageDialog.openInformation(null, "Information", "PIM has been generated !");
    }


}
