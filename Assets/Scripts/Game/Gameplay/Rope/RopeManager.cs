using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game.Gameplay {

    public sealed class RopeManager : MonoBehaviour {

        [SerializeField]
        private float _length;

        [SerializeField]
        private float _mass;

        [SerializeField]
        private float _width;

        [SerializeField]
        private int _jointsNumber;

        [SerializeField]
        private Rigidbody2D _attachedBody;

        [SerializeField]
        private LineRenderer _lineRenderer;

        [SerializeField]
        private RopeJoint _jointPrefab;


        private List<RopeJoint> _joints;


        private void Awake() {
            _joints = new List<RopeJoint>();
            _jointsNumber = _jointsNumber < 2 ? 2 : _jointsNumber;
            _length = _length < 1f ? 1f : _length;
            _mass = _mass < 0.01f ? 0.01f : _mass;
            var distance = _length / _jointsNumber;
            var mass = _mass / _jointsNumber;

            var previousJoint = Instantiate(_jointPrefab, _attachedBody.transform.position + Vector3.down * distance, Quaternion.identity, transform);
            previousJoint.Mass = mass;
            previousJoint.Distance = distance;
            previousJoint.Width = _width;
            previousJoint.ConnectBody(_attachedBody);
            _joints.Add(previousJoint);

            for (int i = 2; i < _jointsNumber; i++) {
                var joint = Instantiate(_jointPrefab, previousJoint.transform.position + Vector3.down * distance, Quaternion.identity, transform);
                joint.Mass = mass;
                joint.Distance = distance;
                joint.Width = _width;
                joint.ConnectBody(previousJoint.Body);
                previousJoint = joint;
                _joints.Add(previousJoint);
            }
        }

        private void FixedUpdate() {
            _length = _length < 1f ? 1f : _length;
            for (int i = 0; i < _joints.Count; i++) {
                _joints[i].Distance = _length / _jointsNumber;
                _joints[i].Width = _width;
            }
        }

        private void LateUpdate() {
            _lineRenderer.positionCount = _joints.Count + 1;
            _lineRenderer.SetPosition(0, _attachedBody.transform.position);
            for (int i = 0; i < _joints.Count; i++) {
                _lineRenderer.SetPosition(i + 1, _joints[i].transform.position);
            }
            _lineRenderer.startWidth = _width;
            _lineRenderer.endWidth = _width;
        }
    }
}