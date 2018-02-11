using UnityEngine;

namespace Game.Gameplay {

    public sealed class RopeJoint : MonoBehaviour {

        public float Mass {
            set { _body.mass = value; }
            get { return _body.mass; }
        }

        public float Distance {
            set { _joint.distance = value; }
            get { return _joint.distance; }
        }

        public float Width {
            set { _collider.radius = value / 2; }
            get { return _collider.radius * 2; }
        }


        [SerializeField]
        private Rigidbody2D _body;

        [SerializeField]
        private SpringJoint2D _joint;

        [SerializeField]
        private CircleCollider2D _collider;


        public void ConnectBody(Rigidbody2D body) {
            _joint.connectedBody = body;
            _joint.connectedAnchor = Vector3.zero;
        }
    }
}