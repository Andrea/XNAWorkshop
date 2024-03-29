using System.Collections.Generic;
using System.Linq;

namespace AStar_Incomplete
{
    public class AStarPathFinder
    {
        private readonly Map _map;
        private readonly List<Node> _openList = new List<Node>();
        private readonly List<Node> _closedList = new List<Node>();
        private Node _currentNode;

        public AStarPathFinder(Map map)
        {
            _map = map;
        }

        public void CalculatePath()
        {
            /*
             * TODO: 
             * 1) Create the first node using the StartCell property in Map, the parent should be null since its the starting node
             * 2) set the _current node to the node you just created
			 * 3) Do you need to add this node to the open list?
             */
            
            while (true)
            {
				if(!_openList.Any())
					return;
                /* TODO				 
                 * 
                 * Find the Node with the smallest F in the open list and set it to _currentNode
				 * 
                 * If that node has the same cell index as the target cell (in _map) break out of the loop                 * 
                 * 
				 * Remove the _currentNode from the open list and add it to the closed list
                 * Add all of the nodes adjacent to the _currentNode to the open list 
				 * 
                 */

                
                ExploreNode(_currentNode, -1, -1, 14);
                ExploreNode(_currentNode, 0, -1, 10);
                ExploreNode(_currentNode, 1, -1, 14);
                ExploreNode(_currentNode, -1, 0, 10);
                ExploreNode(_currentNode, 1, 0, 10);
                ExploreNode(_currentNode, -1, 1, 14);
                ExploreNode(_currentNode, 0, 1, 10);
                ExploreNode(_currentNode, 1, 1, 14);
            }
            _map.HighlightPath(_currentNode);
        }

        private Node FindNodeWithSmallestF(List<Node> nodes)
        {
            int smallestF = nodes.Min(x => x.F);
            return nodes.FirstOrDefault(x => x.F == smallestF);
        }

        private void ExploreNode(Node parentNode, int columnOffset, int rowOffset, int gCost)
        {
            var adjacentCellIndex = _map.GetAdjacentCellIndex(parentNode.CellIndex, columnOffset, rowOffset);

            // Ignore unwalkable nodes (or nodes outside the grid)
            if (adjacentCellIndex == -1)
                return;

            // Ignore nodes on the closed list 
			if (_closedList.Any(n => n.CellIndex == adjacentCellIndex))
				return;

	        UpdatePathCost(adjacentCellIndex, parentNode, gCost);

        	AddAdjacentCellToOpenList(parentNode, gCost, adjacentCellIndex);
        }

    	private void AddAdjacentCellToOpenList(Node parentNode, int gCost, int cellIndex)
    	{

			/* TODO: 
			 * 1) Create a new node with the cellIndex and parentNode, include the G and H values for the node
			 * 2) calculate F for the node
			 * 2) finally add to open list
			 */
    		
    	}

    	private void UpdatePathCost(int cellIndex, Node parentNode, int gCost)
    	{
    		var adjacentNode = _openList.SingleOrDefault(n => n.CellIndex == cellIndex);
    		
    		if (parentNode.G + gCost < adjacentNode.G)
    		{
    			adjacentNode.Parent = parentNode;
    			/*
					* TODO: calculate the values of G and H for adjacentNode
					*/
    				
    		}
    		
    	}
    }
}