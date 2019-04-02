package org.modelio.microservicesnetcore.code.generator.handler;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.code.generator.template.HbmTemplate;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;

public class GenerateHbmFilesHandler extends HandlerAdapter 
{
	private String _path;
	private HbmTemplate _template;
	private List<String> _mappingFiles;

	public GenerateHbmFilesHandler(String applicationName,Package domain,String path,List<String> mappingFiles)  {
		_path=path+"\\RepositoriesNH\\Config";
		// créer le répertoire si il n'existe pas
		File fileDir = new File(_path);
		fileDir.mkdirs();
		_template=new HbmTemplate(applicationName, domain);
		_mappingFiles=mappingFiles;
	}

	@Override
	protected void beginVisitingClassifier(Classifier visited) {
		
		StringBuffer content = new StringBuffer("");
		content.append(_template.getHeader(visited));
		for (Attribute attr : visited.getOwnedAttribute()) {
			if (!attr.getName().equals("id"))
				content.append(_template.getAttribute(attr));
		}
		for (AssociationEnd end: visited.getOwnedEnd()) {
			if (isOneToOne(end)) content.append(_template.getOnetoone(end));
			if (isOneToMany(end)) content.append(_template.getOnetomany(end));
			if (isManyToOne(end)) content.append(_template.getManytoone(end));
			if (isManyToMany(end)) content.append(_template.getManytomany(end));
		}
		content.append(_template.getEnd());
		
		
		try {
			File fileDir = new File(_path );
			fileDir.mkdirs();
			File file = new File(_path + "\\" + visited.getName() + ".hbm.xml");
			FileWriter writer = new FileWriter(file);
			writer.write(content.toString());
			writer.close();
			_mappingFiles.add(visited.getName() + ".hbm.xml");
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private boolean isOneToOne(AssociationEnd end) {
		boolean result = false;
		if (!end.getOpposite().getMultiplicityMax().equals("*") && !end.getMultiplicityMax().equals("*"))
			result = true;
		return result;
	}
	private boolean isOneToMany(AssociationEnd end) {
		boolean result = false;
		if (!end.getOpposite().getMultiplicityMax().equals("*") && end.getMultiplicityMax().equals("*"))
			result = true;
		return result;
	}
	private boolean isManyToOne(AssociationEnd end) {
		boolean result = false;
		if (end.getOpposite().getMultiplicityMax().equals("*") && !end.getMultiplicityMax().equals("*"))
			result = true;
		return result;
	}
	private boolean isManyToMany(AssociationEnd end) {
		boolean result = false;
		if (end.getOpposite().getMultiplicityMax().equals("*") && end.getMultiplicityMax().equals("*"))
			result = true;
		return result;
	}
	
}
