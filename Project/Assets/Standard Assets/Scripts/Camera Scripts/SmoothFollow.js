
var target : Transform;


function LateUpdate () {
	// Early out if we don't have a target
	if (!target)
		return;
	
	transform.position += (target.position-transform.position - target.forward*10+target.up*4)/20.0;

	// Always look at the target
	//transform.LookAt (target);
}