package pacman.entries.pacman;

import java.util.HashMap;
import java.util.Map;

public class Node {

	boolean isLeafNode = true;
	String label = "";
	HashMap<String, Node> childrenNodes = new HashMap<String, Node>();
	
	public Node() {}
	
	public Node(String label) {
		this.label = label;
	}
	
	public void setLabel(String label) {
		this.label = label;
	}
	
	public String getLabel() {
		return this.label;
	}

	public boolean isLeafNode() {
		return isLeafNode;
	}
	
	public void addChild(String value, Node childNode) {
		childrenNodes.put(value, childNode);
		this.isLeafNode = false;
	}
	
	public Node getChild(String key) {
		return childrenNodes.get(key);
	}
	
	public HashMap<String, Node> getChildren() {
		return childrenNodes;
	}
	
	public String getName() {
		return this.label;
	}
	
	public void Print(){
		Print("");
	}
	
	private void Print(String indent) {

        if (this.isLeafNode) {
            System.out.print(indent);
            System.out.println("  └─ Return " + getLabel());
        }
        Map.Entry<String, Node>[] nodes = childrenNodes.entrySet().toArray(new Map.Entry[0]);
        for (int i = 0; i < nodes.length; i++) {
        	System.out.print(indent);
            if (i == nodes.length - 1) {
                System.out.println("└─ \"" + label + "\" = " + nodes[i].getKey() + ":");
                nodes[i].getValue().Print(indent + "    ");
            } else {
                System.out.println("├─ \"" + label + "\" = " + nodes[i].getKey() + ":");
                nodes[i].getValue().Print(indent + (char)0x007C +"   ");
            }
        }
    }
}
