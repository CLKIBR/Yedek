from setup.setup_assets import setup_assets
from scheduler.task_scheduler import run_scheduler

if __name__ == "__main__":
    setup_assets()
    run_scheduler()
