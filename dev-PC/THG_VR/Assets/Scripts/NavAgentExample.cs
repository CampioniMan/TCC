using UnityEngine;
using System.Collections;

// ----------------------------------------------------------
// CLASSE	:	NavAgentExample
// DESC		:	Faz o comportamento para testar o NavMeshAgent do Unity
// ----------------------------------------------------------
[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentExample : MonoBehaviour 
{
	// Variáveis do Inspector
	public AIWaypointNetwork WaypointNetwork = null;
	public int				 CurrentIndex	 = 0;
	public bool				 HasPath		 = false;
	public bool				 PathPending	 = false;
	public bool				 PathStale		 = false;
	public NavMeshPathStatus PathStatus      = NavMeshPathStatus.PathInvalid;
	public AnimationCurve	 JumpCurve		 = new AnimationCurve();

	// variáveis private
	private NavMeshAgent _navAgent = null;

	// -----------------------------------------------------
	// Nome :	Start
	// Desc	:	bota NavMeshAgent numa variável e seta o destino inicial
	// -----------------------------------------------------
	void Start () 
	{
		// setando NavMeshAgent
		_navAgent = GetComponent<NavMeshAgent>();

		// para desligar auto-update
		/*_navAgent.updatePosition = false;
		_navAgent.updateRotation = false;*/


		// Se não tem um Waypoint Network válido então retorna
		if (WaypointNetwork==null) return;

		// Seta primeiro waypoint
		SetNextDestination ( false );
	}

	// -----------------------------------------------------
	// Name	:	SetNextDestination
	// Desc :	Incrementa (opcionalmente) o index do próximo
	//			waypoint de destino e manda ir pro próximo, se tiver.
	// -----------------------------------------------------
	void SetNextDestination ( bool incremento )
	{
		// Se não tem network, returna
		if (!WaypointNetwork) return;

		// calcula quanto o waypoint atual (index) precisa ser incrementado
		int incStep = incremento?1:0;
		Transform nextWaypointTransform = null;

		// Calcula o index do próximo waypoint (de modo circular) e coloca o alvo sendo ele
		int nextWaypoint = (CurrentIndex+incStep>=WaypointNetwork.Waypoints.Count)?0:CurrentIndex+incStep;
		nextWaypointTransform = WaypointNetwork.Waypoints[nextWaypoint];

		// se temos um waypoint válido
		if (nextWaypointTransform!=null)
		{
			// Atualizamos o index, colocamos ele como destino e retornamos
			CurrentIndex = nextWaypoint;
			_navAgent.destination = nextWaypointTransform.position;
			return;
		}

		// Não tem um waypoint válido na lista para essa iteração...
		CurrentIndex=nextWaypoint;
	}

	// ---------------------------------------------------------
	// Name	:	Update
	// Desc	:	É chamado em cada frame do Unity (x60 por segundo)
	// ---------------------------------------------------------
	void Update () 
	{
		// copia o estado do NavMeshAgent para as variáveis visíveis no Inspector
		HasPath 	= _navAgent.hasPath;
		PathPending = _navAgent.pathPending;
		PathStale	= _navAgent.isPathStale;
		PathStatus	= _navAgent.pathStatus;

		// se o agente está num offMeshLink, então fazemos um pulo
		if (_navAgent.isOnOffMeshLink)
		{
			StartCoroutine( Jump( 1.0f) );
			return;
		}

		// Se nós não temos um caminho e não está pendente, então setamos o 
		// próximo waypoint como alvo, caso contrário se o caminho é obsoleto, re-geramos o caminho
		if ( ( _navAgent.remainingDistance<=_navAgent.stoppingDistance && !PathPending) || PathStatus==NavMeshPathStatus.PathInvalid /*|| PathStatus==NavMeshPathStatus.PathPartial*/)
		{
			SetNextDestination ( true );
		}
		else
			if (_navAgent.isPathStale)
				SetNextDestination ( false );
	}

	// ---------------------------------------------------------
	// Name	:	Jump
	// Desc :	Pulando se estiver num OffMeshLink
	// ---------------------------------------------------------
	IEnumerator Jump ( float duration )
	{
		OffMeshLinkData 	data 		= _navAgent.currentOffMeshLinkData;
		Vector3				startPos	= _navAgent.transform.position;
		Vector3				endPos		= data.endPos + ( _navAgent.baseOffset * Vector3.up);
		float 				time		= 0.0f;

		while ( time<= duration )
		{
			float t = time/duration;
			_navAgent.transform.position = Vector3.Lerp( startPos, endPos, t ) + (JumpCurve.Evaluate(t) * Vector3.up);
			time += Time.deltaTime;
			yield return null;// não sabe o que é yield return?
		}
		_navAgent.transform.position = endPos;
		_navAgent.CompleteOffMeshLink();
	}
}
