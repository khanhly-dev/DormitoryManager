import { Component, OnInit } from '@angular/core';
import { BaseStatDto } from 'src/app/dto/output-dto';
import { DashboardServiceProxy } from 'src/app/service/admin-service/dashboard-service-proxy';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  baseStat! : BaseStatDto;

  constructor(private dashboardService : DashboardServiceProxy) { }

  ngOnInit(): void {
    this.getBaseStat();
  }

  getBaseStat()
  {
    this.dashboardService.getBaseStat().subscribe(x => {
      this.baseStat = x;
    })
  }
   // bieu do tron
  chartType1 = 'pie';

  chartDatasets1 = [
    { data: [300, 50, 100, 40, 120], label: 'My First dataset' }
  ];

  chartLabels1 = ['Red', 'Green', 'Yellow', 'Grey', 'Dark Grey'];

  chartColors1 = [
    {
      backgroundColor: ['#F7464A', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'],
      hoverBackgroundColor: ['#FF5A5E', '#5AD3D1', '#FFC870', '#A8B3C5', '#616774'],
      borderWidth: 2,
    }
  ];

  chartOptions1: any = {
    responsive: true
  };

  chartClicked1(event: any): void {
    console.log(event);
  }

  chartHovered1(event: any): void {
    console.log(event);
  }
  //-------------------------------------------------------------
  //bieu do cot 1
  chartType2 = 'bar';

  chartDatasets2 = [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'My First dataset' }
  ];

  chartLabels2 = ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'];

  chartColors2 = [
    {
      backgroundColor: [
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(255, 206, 86, 0.2)',
        'rgba(75, 192, 192, 0.2)',
        'rgba(153, 102, 255, 0.2)',
        'rgba(255, 159, 64, 0.2)'
      ],
      borderColor: [
        'rgba(255,99,132,1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)'
      ],
      borderWidth: 2,
    }
  ];

  chartOptions2: any = {
    responsive: true
  };

  chartClicked2(event: any): void {
    console.log(event);
  }

  chartHovered2(event: any): void {
    console.log(event);
  }
  //------------------------------------------------------------
  //bieu do cot 2
  chartType3 = 'bar';

  chartDatasets3 = [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'My First dataset' }
  ];

  chartLabels3 = ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'];

  chartColors3 = [
    {
      backgroundColor: [
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(255, 206, 86, 0.2)',
        'rgba(75, 192, 192, 0.2)',
        'rgba(153, 102, 255, 0.2)',
        'rgba(255, 159, 64, 0.2)'
      ],
      borderColor: [
        'rgba(255,99,132,1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)'
      ],
      borderWidth: 2,
    }
  ];

  chartOptions3: any = {
    responsive: true
  };

  chartClicked3(event: any): void {
    console.log(event);
  }

  chartHovered3(event: any): void {
    console.log(event);
  }
  //-----------------------------------------------------------\
  //bieu do hang
  chartType4 = 'horizontalBar';

  chartDatasets4 = [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'My First dataset' }
  ];

  chartLabels4 = ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'];

  chartColors4 = [
    {
      backgroundColor: [
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(255, 206, 86, 0.2)',
        'rgba(75, 192, 192, 0.2)',
        'rgba(153, 102, 255, 0.2)',
        'rgba(255, 159, 64, 0.2)'
      ],
      borderColor: [
        'rgba(255,99,132,1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)'
      ],
      borderWidth: 2,
    }
  ];

  chartOptions4: any = {
    responsive: true
  };

  chartClicked4(event: any): void {
    console.log(event);
  }

  chartHovered4(event: any): void {
    console.log(event);
  }
  //-----------------------------------------------------------
}
