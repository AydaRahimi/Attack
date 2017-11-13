using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class handles the Attack function
/// </summary>
///

namespace s3
{
	public class Enemy_Attack: NetworkBehaviour{
		private Enemy_Master EnemyMaster;
		private Transform attackTarget;
		private Transform myTransform;
		public float attackRae=1;
		private float nextAttack;
		public float attackRange;
		public int myAction=0;

        //Initialize
        void OnEnable()
		{
			SetInitialReferences();
			enemyMaster.EventEnemyDie +=DisableThis;
			enemyMaster.EventEnemySetNavTarget +=SetAttackTarget;
		}

        //Disabling
		void OnDisable()
		{
			enemyMaster.EventEnemyDie +=DisableThis;
            //disable=attkbut.SetActive(false);
		}


		//the update is called once in each frame
		void update()
		{
			TryToAttack();
		}

		void SetInitialReferences()
		{
			enemyMaster=GetComponent<Enemy_Master>();
			myTransform=Transform;
		}

		void SetAttackTarget(Transform targetTransform)
		{
			attackTarget=targetTransform;
		}

		void TryToAttack()
		{
			if(attackTarget != null)
			{
				if(Time.time>nextAttack)
				{
					nextAttack=Time.time+attackRate;
					if(Vector3.Distance(myTransform.positio,attackTarget.position)<=attackRange)
					{
						//you want the enemy to look at its onw height without getting tilted/ for looking straight aghead
						Vector3 lookAtVector=new Vector3(attackTarget.position.x,myTransform.position.y,attackTarget.position.z);
						myTransform.lookAt(lookAtVector);
						enemyMaster.CallEventEnemyAttack();
						enemyMaster.isOnRoute=false;

					}
				}
			}
		}			




		void DisableThis()
		{

		}


        /* SPAWNING MAT

        var objectToSpawn : Transform;
        var canReSpawn : true;

        function attkbut ()
        {
            if(myAction==2) //we wait for player to click on the attackable area
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
         */