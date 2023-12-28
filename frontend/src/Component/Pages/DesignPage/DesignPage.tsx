import React from 'react'
import Table from '../../Table/Table'
import { CompanyKeyMetrics } from '../../../company';
import RatioList from '../../RatioList/RatioList';
import { TestDataCompany } from '../../Table/TestData';

type Props = {}

const data = TestDataCompany;
const tableConfig = [
    {
      label: "Market Cap",
      render: (company: CompanyKeyMetrics) => company.marketCapTTM,
    }
  ];

const DesignPage = (props: Props) => {
        return (
          <>
            <h1>
              Design guide- This is the design guide for Fin Shark. These are reuable
              components of the app with brief instructions on how to use them.
            </h1>
            <RatioList config={tableConfig} data={data} />
            <Table config={tableConfig} data={data} />
            <h3>
              Table - Table takes in a configuration object and company data as
              params. Use the config to style your table.
            </h3>
          </>
        );
      }

export default DesignPage;