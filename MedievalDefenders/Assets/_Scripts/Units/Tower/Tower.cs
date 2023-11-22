using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _rotationPoint;
    [SerializeField] private GameObject _range;
    [SerializeField] private Transform _firingPoint;
    [SerializeField] private GameObject _prefabProjectile;

    [SerializeField] private LayerMask _layerMaskEnemies;

    [Header("Atributes")]
    public float targetingRange = 5f;
    public float rotationSpeed = 100f;
    public float bulletsPerSecond = 1f;
    public int price = 100;


    private Transform _target;
    private float _timeUntilFire = 0f;
    private GameObject _projectiles;

    public Projectile GetProjectile() {
        return _prefabProjectile.GetComponent<Projectile>();
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, _layerMaskEnemies);
        if (hits.Length > 0)
        {
            _target = hits[0].transform;
        }
    }

    private Quaternion RotationTowardsTarget()
    {
        float angle = Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        float rotacao = 0f;

        if (angle >= -180 && angle < 0) {
            rotacao = 180f;
            angle = -(angle + 180f);
        }

        // angulo do boneco
        Quaternion currentRotation = gameObject.transform.localRotation;
        currentRotation.y = rotacao;
        gameObject.transform.localRotation = currentRotation;

        // angulo da arma
        Quaternion targetRotation = Quaternion.Euler(new Vector3(rotacao, 0f, angle));

        return Quaternion.RotateTowards(_rotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool TargetIsInRange()
    {
        return Vector2.Distance(_target.position, transform.position) <= targetingRange;
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate<Projectile>(_prefabProjectile.GetComponent<Projectile>(), _firingPoint.position, Quaternion.identity, _projectiles.transform);
        projectile.SetTarget(_target);
    }

    private void Update()
    {
        if (_target == null)
        {
            FindTarget();
            return;
        }

        _rotationPoint.rotation = RotationTowardsTarget();

        if (!TargetIsInRange())
        {
            _target = null;
            return;
        }

        _timeUntilFire += Time.deltaTime;
        if (_timeUntilFire >= 1f / bulletsPerSecond)
        {
            Shoot();
            _timeUntilFire = 0f;
        }

    }

    private void Start()
    {
        _projectiles = GameObject.Find("Projectiles");
        _range.transform.localScale = new(targetingRange, targetingRange);
        this.ShowRange();
    }

    public void ShowRange()
    {
        _range.SetActive(true);
    }

    public void HideRange()
    {
        _range.SetActive(false);
    }
}
