using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary> Astarで使用するノードデータ </summary>
public struct Node
{
    /// <summary>ノードのポジション </summary>
    internal Vector2Int NodeId { get; }
    /// <summary>このノードにたどり着く前のノードポジション </summary>
    internal Vector2Int FromNodeId { get; private set; }
    /// <summary>経路として使用できないフラグ </summary>
    internal bool IsLock { get; private set; }
    /// <summary>ノードの有無 </summary>
    internal bool IsActive { get; private set; }
    /// <summary>必要コスト</summary>
    internal double MoveCost { get; private set; }
    /// <summary>ヒューリスティックなコスト </summary>
    private double _heuristicCost;
    /// <summary>からのノードの作成 </summary>
    internal static Node CreateBlankNode(Vector2Int position)
    {
        return new Node(position, new Vector2Int(-1, 1));
    }
    /// <summary>ノードの生成</summary>
    internal static Node CreateNode(Vector2Int position, Vector2Int goalPosition)
    {
        return new Node(position, goalPosition);
    }
    /// <summary>CreateBlankNode, CreateNodeを使用する </summary>
    internal Node(Vector2Int nodeId, Vector2Int goalNodeId) : this()
    {
        NodeId = nodeId;
        IsLock = false;
        Remove();
        MoveCost = 0;
        UpdateGoalNodeId(goalNodeId);
    }
    /// <summary>ゴールの更新 ヒューリスティックコストの更新 </summary>
    internal void UpdateGoalNodeId(Vector2Int goal)
    {
        //直線距離をヒューリスティックコストとする
        _heuristicCost = Mathf.Sqrt(
            Mathf.Pow(goal.x - NodeId.x, 2) +
            Mathf.Pow(goal.y - NodeId.y, 2)
            );
    }

    internal double GetScore()
    {
        return MoveCost + _heuristicCost;
    }
    internal void SetFromNodeId(Vector2Int value)
    {
        FromNodeId = value;
    }
    internal void Remove()
    {
        IsActive = false;
    }
    internal void Add()
    {
        IsActive = true;
    }
    internal void SetMoveCost(double cost)
    {
        MoveCost = cost;
    }
    internal void SetIsLock(bool isLock)
    {
        IsLock = isLock;
    }
    internal void Clear()
    {
        Remove();
        MoveCost = 0;
        UpdateGoalNodeId(new Vector2Int(-1, 1));
    }
}
public class Astar : MonoBehaviour
{
    private int _fieldSize;
    private Node[,] _nodes;
    private Node[,] _openNodes;
    private Node[,] _closedNodes;
    /// <summary>
    /// 斜め移動の場合のコスト
    /// </summary>
    private float _diagonalMoveCost;
    /// <summary>
    /// 使用する前に実行して初期化してください
    /// </summary>
    public void Initialize(int size)
    {
        _fieldSize = size;
        _nodes = new Node[_fieldSize, _fieldSize];
        _openNodes = new Node[_fieldSize, _fieldSize];
        _closedNodes = new Node[_fieldSize, _fieldSize];
        SetDiagonalMoveCost(Mathf.Sqrt(2f));
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                _nodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
                _openNodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
                _closedNodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
            }
        }
    }
    public void SetDiagonalMoveCost(float cost)
    {
        _diagonalMoveCost = cost;
    }
    /// <summary>
    /// ルート検索開始
    /// </summary>
    public bool SearchRoute(Vector2Int startNodeId, Vector2Int goalNodeId, List<Vector2Int> routeList)
    {
        ResetNode();
        if (startNodeId == goalNodeId)
        {
            Debug.Log($"{startNodeId}/{goalNodeId}/同じ場所なので終了");
            return false;
        }
        // 全ノード更新
        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                _nodes[x, y].UpdateGoalNodeId(goalNodeId);
                _openNodes[x, y].UpdateGoalNodeId(goalNodeId);
                _closedNodes[x, y].UpdateGoalNodeId(goalNodeId);
            }
        }
        // スタート地点の初期化
        _openNodes[startNodeId.x, startNodeId.y] = Node.CreateNode(startNodeId, goalNodeId);
        _openNodes[startNodeId.x, startNodeId.y].SetFromNodeId(startNodeId);
        _openNodes[startNodeId.x, startNodeId.y].Add();
        while (true)
        {
            var bestScoreNodeId = GetBestScoreNodeId();
            OpenNode(
                bestScoreNodeId,
                goalNodeId
            );
            // ゴールに辿り着いたら終了
            if (bestScoreNodeId == goalNodeId)
            {
                break;
            }
        }
        ResolveRoute(startNodeId, goalNodeId, routeList);
        return true;
    }
    void ResetNode()
    {
        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                _nodes[x, y].Clear();
                _openNodes[x, y].Clear();
                _closedNodes[x, y].Clear();
            }
        }
    }
    // ノードを展開する
    void OpenNode(Vector2Int bestNodeId, Vector2Int goalNodeId)
    {
        // 4方向走査
        for (int dx = -1; dx < 2; dx++)
        {
            for (int dy = -1; dy < 2; dy++)
            {
                int cx = bestNodeId.x + dx;
                int cy = bestNodeId.y + dy;
                if (CheckOutOfRange(dx, dy, bestNodeId.x, bestNodeId.y) == false)
                {
                    continue;
                }
                if (_nodes[cx, cy].IsLock)
                {
                    continue;
                }
                // 縦横で動く場合はコスト : 1
                // 斜めに動く場合はコスト : _diagonalMoveCost
                var addCost = dx * dy == 0 ? 1 : _diagonalMoveCost;
                _nodes[cx, cy].SetMoveCost(_openNodes[bestNodeId.x, bestNodeId.y].MoveCost + addCost);
                _nodes[cx, cy].SetFromNodeId(bestNodeId);
                // ノードのチェック
                UpdateNodeList(cx, cy, goalNodeId);
            }
        }
        // 展開が終わったノードは closed に追加する
        _closedNodes[bestNodeId.x, bestNodeId.y] = _openNodes[bestNodeId.x, bestNodeId.y];
        // closedNodesに追加
        _closedNodes[bestNodeId.x, bestNodeId.y].Add();
        // openNodesから削除
        _openNodes[bestNodeId.x, bestNodeId.y].Remove();
    }
    /// <summary>
    /// 走査範囲内チェック
    /// </summary>
    bool CheckOutOfRange(int dx, int dy, int x, int y)
    {
        if (dx == 0 && dy == 0)
        {
            return false;
        }
        int cx = x + dx;
        int cy = y + dy;
        if (cx < 0
            || cx == _fieldSize
            || cy < 0
            || cy == _fieldSize
        )
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// ノードリストの更新
    /// </summary>
    void UpdateNodeList(int x, int y, Vector2Int goalNodeId)
    {
        if (_openNodes[x, y].IsActive)
        {
            // より優秀なスコアであるならMoveCostとfromを更新する
            if (_openNodes[x, y].GetScore() > _nodes[x, y].GetScore())
            {
                // Node情報の更新
                _openNodes[x, y].SetMoveCost(_nodes[x, y].MoveCost);
                _openNodes[x, y].SetFromNodeId(_nodes[x, y].FromNodeId);
            }
        }
        else if (_closedNodes[x, y].IsActive)
        {
            // より優秀なスコアであるなら closedNodesから除外しopenNodesに追加する
            if (_closedNodes[x, y].GetScore() > _nodes[x, y].GetScore())
            {
                _closedNodes[x, y].Remove();
                _openNodes[x, y].Add();
                _openNodes[x, y].SetMoveCost(_nodes[x, y].MoveCost);
                _openNodes[x, y].SetFromNodeId(_nodes[x, y].FromNodeId);
            }
        }
        else
        {
            _openNodes[x, y] = new Node(new Vector2Int(x, y), goalNodeId);
            _openNodes[x, y].SetFromNodeId(_nodes[x, y].FromNodeId);
            _openNodes[x, y].SetMoveCost(_nodes[x, y].MoveCost);
            _openNodes[x, y].Add();
        }
    }
    void ResolveRoute(Vector2Int startNodeId, Vector2Int goalNodeId, List<Vector2Int> result)
    {
        if (result == null)
        {
            // 本来はGCを発生させないために生成済みのリストを渡す
            result = new List<Vector2Int>();
        }
        else
        {
            result.Clear();
        }
        var node = _closedNodes[goalNodeId.x, goalNodeId.y];
        result.Add(goalNodeId);
        int cnt = 0;
        // 捜査トライ回数を1000と決め打ち(無限ループ対応)
        int tryCount = 1000;
        bool isSuccess = false;
        while (cnt++ < tryCount)
        {
            var beforeNode = result[0];
            if (beforeNode == node.FromNodeId)
            {
                // 同じポジションなので終了
                Debug.LogError("同じポジションなので終了失敗" + beforeNode + " / " + node.FromNodeId + " / " + goalNodeId);
                break;
            }
            if (node.FromNodeId == startNodeId)
            {
                isSuccess = true;
                break;
            }
            else
            {
                // 開始座標は結果リストには追加しない
                result.Insert(0, node.FromNodeId);
            }
            node = _closedNodes[node.FromNodeId.x, node.FromNodeId.y];
        }
        if (isSuccess == false)
        {
            Debug.LogError("失敗" + startNodeId + " / " + node.FromNodeId);
        }
    }
    /// <summary>
    /// 最良のノードIDを返却
    /// </summary>
    Vector2Int GetBestScoreNodeId()
    {
        var result = new Vector2Int(0, 0);
        double min = double.MaxValue;
        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                if (_openNodes[x, y].IsActive == false)
                {
                    continue;
                }
                if (min > _openNodes[x, y].GetScore())
                {
                    // 優秀なコストの更新(値が低いほど優秀)
                    min = _openNodes[x, y].GetScore();
                    result = _openNodes[x, y].NodeId;
                }
            }
        }
        return result;
    }
    /// <summary>
    /// ノードのロックフラグを変更
    /// </summary>
    public void SetLock(Vector2Int lockNodeId, bool isLock)
    {
        _nodes[lockNodeId.x, lockNodeId.y].SetIsLock(isLock);
    }
}
