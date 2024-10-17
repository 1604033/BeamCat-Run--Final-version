using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{	
	[AddComponentMenu("Corgi Engine/Environment/Falling Platform")]
	public class FallingPlatforms : CorgiMonoBehaviour 
	{
		[Tooltip("the time (in seconds) before the fall of the platform")]
		public float TimeBeforeFall = 2f;

		[Tooltip("the speed at which the platforms falls")]
		public float FallSpeed = 2f;

		[Tooltip("the tolerance to apply when comparing the relative positions of the falling platform and ")]
		public float Tolerance = 0.1f;

		[Tooltip("if this is true, the platform will only fall if the colliding character is above the platform")]		
		public bool RequiresCharacterAbove = true;

		[Tooltip("Time in seconds before the platform respawns after falling")]
		public float RespawnDelay = 4f; // Time to wait before respawning the platform

		protected Animator _animator;
		protected bool _shaking = false;
		protected Vector2 _newPosition;
		protected Bounds _bounds;
		protected Collider2D _collider2D;
		protected Vector3 _initialPosition;
		protected float _timer;
		protected float _platformTopY;
		protected AutoRespawn _autoRespawn;
		protected bool _isFalling = false; // Track if the platform is currently falling
		protected Renderer _renderer;

		protected virtual void Start()
		{
			Initialization ();
		}

		protected virtual void Initialization()
		{
			_animator = this.gameObject.GetComponent<Animator>();
			_collider2D = this.gameObject.GetComponent<Collider2D> ();
			_renderer = this.gameObject.GetComponent<Renderer>();
			_autoRespawn = this.gameObject.GetComponent<AutoRespawn> ();
			_bounds = LevelManager.Instance.LevelBounds;
			_initialPosition = this.transform.position;
			_timer = TimeBeforeFall;
		}
		
		protected virtual void FixedUpdate()
		{		
			UpdateAnimator ();

			if (_timer < 0 && !_isFalling)
			{
				_newPosition = new Vector2(0, -FallSpeed * Time.deltaTime);
				transform.Translate(_newPosition, Space.World);

				// Check if the platform has fallen below the bounds
				if (transform.position.y < _bounds.min.y)
				{
					DisableFallingPlatform();
				}
			}
		}

		protected virtual void DisableFallingPlatform()
		{
			_isFalling = true; // Mark platform as falling
			
			// Disable the renderer and collider to simulate the platform disappearing
			_renderer.enabled = false;
			_collider2D.enabled = false;

			StartCoroutine(RespawnPlatform()); // Start the respawn coroutine
		}

		// Coroutine to handle respawning the platform after a delay
		protected IEnumerator RespawnPlatform()
		{
			yield return new WaitForSeconds(RespawnDelay);

			this.transform.position = _initialPosition; // Reset the platform's position
			_timer = TimeBeforeFall; // Reset the fall timer
			_shaking = false; // Stop shaking

			// Re-enable the renderer and collider
			_renderer.enabled = true;
			_collider2D.enabled = true;

			_isFalling = false; // Mark platform as no longer falling
		}

		protected virtual void UpdateAnimator()
		{				
			if (_animator != null)
			{
				_animator.SetBool("Shaking", _shaking);	
			}
		}

		public virtual void OnTriggerStay2D(Collider2D collider)
		{
			CorgiController controller = collider.GetComponent<CorgiController>();
			if (controller == null)
				return;
			
			if (TimeBeforeFall > 0)
			{
				bool canShake = false;

				if (RequiresCharacterAbove)
				{
					_platformTopY = (_collider2D != null) ? _collider2D.bounds.max.y : this.transform.position.y;
					if (controller.ColliderBottomPosition.y >= _platformTopY - Tolerance)
					{
						canShake = true;
					}	
				}
				else
				{
					canShake = true;
				}

				if (canShake)
				{
					_timer -= Time.deltaTime;
					_shaking = true;
				}
			}
			else
			{
				_shaking = false;
			}
		}

		protected virtual void OnTriggerExit2D(Collider2D collider)
		{
			CorgiController controller = collider.GetComponent<CorgiController>();
			if (controller == null)
				return;
			
			_shaking = false;
		}

		protected virtual void OnRevive()
		{
			this.transform.position = _initialPosition;		
			_timer = TimeBeforeFall;
			_shaking = false;
		}

		protected virtual void OnEnable ()
		{
			if (gameObject.MMGetComponentNoAlloc<AutoRespawn>() != null)
			{
				gameObject.MMGetComponentNoAlloc<AutoRespawn>().OnRevive += OnRevive;
			}
		}

		protected virtual void OnDisable()
		{
			if (_autoRespawn != null)
			{
				_autoRespawn.OnRevive -= OnRevive;
			}			
		}
	}
}
