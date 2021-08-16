import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Health } from "../models/health";
import { Measurements } from "../models/measurements";
import { TylerTrackerApi } from "../services/tyler-tracker-api";

@Component({
  selector: 'app-basic-example',
  templateUrl: './health-main.component.html'
})
export class HealthMainComponent implements OnInit {
  public formGroup: FormGroup;
  public currentDate: string;
  public isFormHidden: boolean;
  public loading: boolean;
  public weightData: any[] = [];
  public chartOptions;

  constructor(private api: TylerTrackerApi) { }

  ngOnInit() {
    this.chartOptions = {
      0: {
        type: 'exponential',
        visibleInLegend: true,
      }
    };
    this.loading = true;
    this.api.getLast6MonthsOfHealthData().subscribe(results => {
      for (var i = 0; i < results.length; i++) {
        const health = results[i];
        const newDate = new Date(health.date);
        this.weightData.push([newDate.toLocaleDateString('en-us').toString(), health.weight]);
      }
      this.loading = false;
    });
    this.formGroup = new FormGroup({
      weight: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[1-9]\d*(\.\d+)?$/gm)
      ]),
      systolic: new FormControl(),
      diastolic: new FormControl(),
      neck: new FormControl(),
      chest: new FormControl(),
      arm: new FormControl(),
      waist: new FormControl(),
    });
    this.currentDate = new Date().toLocaleDateString();
  }

  public onSubmit() {
    if (this.formGroup.valid) {
      const weight = this.formGroup.controls['weight'].value;
      const systolic = this.formGroup.controls['systolic'].value;
      const diastolic = this.formGroup.controls['diastolic'].value;
      const neck = this.formGroup.controls['neck'].value;
      const chest = this.formGroup.controls['chest'].value;
      const arm = this.formGroup.controls['arm'].value;
      const waist = this.formGroup.controls['waist'].value;

      let health: Health = {
        date: new Date(),
        weight: weight
      }

      if (systolic > 0) {
        health.systolic = systolic;
        health.diastolic = diastolic;
      }

      if (neck > 0) {
        const measurments: Measurements = {
          neck: neck,
          chest: chest,
          arm: arm,
          waist: waist
        }
        health.measurements = measurments;
      }

      if (health) {
        this.weightData.push([health.date, health.date]);
      }

      this.loading = true;
      this.api.createHealthData(health).subscribe(result => {
        this.loading = false;
      });
      this.isFormHidden = true;
    }
  }
}
