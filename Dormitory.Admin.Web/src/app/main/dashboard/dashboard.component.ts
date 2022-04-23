import { Component, OnInit } from '@angular/core';
import { BaseStatDto, FeeDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { DashboardServiceProxy } from 'src/app/service/admin-service/dashboard-service-proxy';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  baseStat!: BaseStatDto;
  listContractFee!: PageResultBase<FeeDto>
  listServiceFee!: PageResultBase<FeeDto>
  pageIndex = 1;
  totalStudent : number = 0;

  constructor(private dashboardService: DashboardServiceProxy) { }

  ngOnInit(): void {
    this.getBaseStat();
    this.getListContractFee("", 1, 3);
    this.getListServiceFee("", 1, 3);
    this.getGenderPercent();
    this.getContractFeeChart();
    this.getServiceFeeChart();
    this.getAreaChart();
  }

  getBaseStat() {
    this.dashboardService.getBaseStat().subscribe(x => {
      this.baseStat = x;
    })
  }
  getListContractFee(keyword: string, pageIndex: number, pageSize: number) {
    this.dashboardService.getListContractFee(keyword, pageIndex, pageSize).subscribe(x => {
      this.listContractFee = x;
    })
  }
  getListServiceFee(keyword: string, pageIndex: number, pageSize: number) {
    this.dashboardService.getListServiceFee(keyword, pageIndex, pageSize).subscribe(x => {
      this.listServiceFee = x;
    })
  }
  getGenderPercent()
  {
    this.dashboardService.getGenderPercent().subscribe(x => {
      this.totalStudent = x.countFemale + x.countMale
      this.chartDatasets1 = [
        { data: [x.countMale, x.countFemale], label: 'Tổng sinh viên ở KTX' }
      ]
    })
  }
  getContractFeeChart()
  {
    this.dashboardService.getContractFeeChart().subscribe(x => {
      this.chartDatasets3 = [
        { data: [x.dept, x.paid, x.totalFee], label: 'Biểu đồ phí hợp đồng' }
      ];
    })
  }
  getServiceFeeChart()
  {
    this.dashboardService.getServiceFeeChart().subscribe(x => {
      this. chartDatasets2 = [
        { data: [x.dept, x.paid, x.totalFee], label: 'Biểu đồ phí điện nước' }
      ];
    })
  }
  getAreaChart()
  {
    this.dashboardService.getAreaChart().subscribe(x => {
      this.chartLabels4 = x.map(x => x.area);
      this. chartDatasets4 = [
        { data: x.map(x => x.studentCount), label: 'Biểu đồ sinh viên ở theo khu' }
      ];
    })
  }
  // bieu do tron
  chartType1 = 'pie';

  chartDatasets1 = [
    { data: [50, 50], label: 'Tổng sinh viên ở KTX' }
  ];

  chartLabels1 = ['Nam', 'Nữ'];

  chartColors1 = [
    {
      backgroundColor: ['#F7464A', '#46BFBD'],
      hoverBackgroundColor: ['#FF5A5E', '#5AD3D1'],
      borderWidth: 2,
    }
  ];

  chartOptions1: any = {
    responsive: true
  };
  //-------------------------------------------------------------
  //bieu do cot 1
  chartType2 = 'horizontalBar';

  chartDatasets2 = [
    { data: [65, 59, 80], label: 'Biểu đồ phí điện nước' }
  ];

  chartLabels2 = ['Chưa đóng', 'Đã đóng', 'Phải thu'];

  chartColors2 = [
    {
      backgroundColor: [
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(255, 206, 86, 0.2)',
      ],
      borderColor: [
        'rgba(255,99,132,1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
      ],
      borderWidth: 2,
    }
  ];

  chartOptions2: any = {
    responsive: true
  };
  //------------------------------------------------------------
  //bieu do cot 2
  chartType3 = 'horizontalBar';

  chartDatasets3 = [
    { data: [65, 59, 80], label: 'Biểu đồ phí hợp đồng' }
  ];

  chartLabels3 = ['Chưa đóng', 'Đã đóng', 'Phải thu'];

  chartColors3 = [
    {
      backgroundColor: [
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(255, 206, 86, 0.2)',
      ],
      borderColor: [
        'rgba(255,99,132,1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
      ],
      borderWidth: 2,
    }
  ];

  chartOptions3: any = {
    responsive: true
  };
  //-----------------------------------------------------------\
  //bieu do hang
  chartType4 = 'bar';

  chartDatasets4 = [
    { data: [0,0,0], label: 'My First dataset' }
  ];

  chartLabels4! : Array<any>;

  chartOptions4: any = {
    responsive: true
  };
  //-----------------------------------------------------------
}
