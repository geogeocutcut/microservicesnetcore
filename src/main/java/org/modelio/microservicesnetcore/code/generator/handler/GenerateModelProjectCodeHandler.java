package org.modelio.microservicesnetcore.code.generator.handler;

import java.io.File;
import java.io.FileWriter;

import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.code.generator.interfaces.InterfaceModelProjectTemplate;
import org.modelio.microservicesnetcore.code.generator.template.ModelNHProjectTemplate;
import org.modelio.microservicesnetcore.code.generator.template.ModelStandardProjectTemplate;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GenerateModelProjectCodeHandler extends HandlerAdapter {
	private String _path;
	private InterfaceModelProjectTemplate _template;
	
	public GenerateModelProjectCodeHandler(String applicationName,Package domain,String path,String daltype)
	{
		_path=path+"\\Model";
		if(daltype.equals("NHibernate"))
			_template=new ModelNHProjectTemplate(applicationName, domain);
		else 
			_template=new ModelStandardProjectTemplate(applicationName, domain);
		
		// créer le répertoire project si il n'existe pas
		File fileDir = new File(_path);
		fileDir.mkdirs();
		
		// créer le csproj
		String name=applicationName+"."+domain.getName()+"Domain.Model.csproj";
		
		StringBuffer content = new StringBuffer("");
		content.append(_template.getCsProj());
		if(content.length()>0)
		{
			try {
				File csprojFile =new File(_path+"\\"+name);
				csprojFile.createNewFile();
				FileWriter writer = new FileWriter(csprojFile);
				try {
	                writer.write(content.toString());
	            } finally {
	                // quoiqu'il arrive, on ferme le fichier
	                writer.close();
	            }
	        } catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
	        }
		}
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) 
	{
		String name=visited.getName()+".cs";
		
		StringBuffer content = new StringBuffer("");
		content.append(_template.getHeader(visited));
		for (Attribute attr : visited.getOwnedAttribute()) {
			if (!attr.getName().equals("id"))
				content.append(_template.getAttribute(attr));
		}
		for (AssociationEnd assoc : visited.getOwnedEnd()) {
			if(assoc.isNavigable())
			{
				String multi =assoc.getMultiplicityMax();
				if(multi.equals("*"))
				{
					content.append(_template.getOnetomany(assoc));
				}
				else
				{
					content.append(_template.getOnetoone(assoc));
				}
			}
		}
		
		content.append(_template.getEnd());
		
		if(content.length()>0)
		{
			try {
				File csFile =new File(_path+"\\"+name);
				csFile.createNewFile();
				FileWriter writer = new FileWriter(csFile);
				try 
				{
	                writer.write(content.toString());
	            } 
				finally 
				{
	                // quoiqu'il arrive, on ferme le fichier
	                writer.close();
	            }
	        }
			catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
	        }
		}
	}
}
